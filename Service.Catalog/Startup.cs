using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Catalog.Application;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Repository;
using Service.Catalog.Repository.IRepository;
using Service.Catalog.Client;
using Service.Catalog.Client.IClient;
using Service.Catalog.Consumers.Error;
using Service.Catalog.Context;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Equipment;
using Service.Catalog.Domain.Parameter;
using Service.Catalog.Domain.Provenance;
using Service.Catalog.Middleware;
using Service.Catalog.Requirements;
using Service.Catalog.Settings;
using Service.MedicalRecord.Client;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Service.Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(Configuration.GetConnectionString("Default"), opt => opt.CommandTimeout(300));
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Scoped);

            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("Default"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IIdentityClient, IdentityClient>();
            services.AddScoped<IPdfClient, PdfClient>();
            services.AddHttpClient<IIdentityClient, IdentityClient>(client =>
            {
                var token = new HttpContextAccessor().HttpContext.Request.Headers["Authorization"].ToString();

                client.BaseAddress = new Uri(Configuration["ClientUrls:Identity"]);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                }

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient<IPdfClient, PdfClient>(client =>
            {
                var token = new HttpContextAccessor().HttpContext.Request.Headers["Authorization"].ToString();

                client.BaseAddress = new Uri(Configuration["ClientUrls:Pdf"]);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                }

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());

                x.UsingRabbitMq((context, configurator) =>
                {
                    var rabbitMQSettings = Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    var queueNames = Configuration.GetSection(nameof(QueueNames)).Get<QueueNames>();

                    configurator.Host(new Uri(rabbitMQSettings.Host), "Catalog", c =>
                    {
                        c.ContinuationTimeout(TimeSpan.FromSeconds(20));
                        c.Username(rabbitMQSettings.Username);
                        c.Password(rabbitMQSettings.Password);
                    });

                    configurator.ReceiveEndpoint(queueNames.BranchError, re =>
                    {
                        re.Consumer<BranchErrorConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(queueNames.CompanyError, re =>
                    {
                        re.Consumer<CompanyErrorConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(queueNames.MaquilaError, re =>
                    {
                        re.Consumer<MaquilaErrorConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(queueNames.MedicError, re =>
                    {
                        re.Consumer<MedicErrorConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Catalog", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });
            });

            var key = Configuration["SecretKey"];
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = tokenKey,
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.RequireHttpsMetadata = false;
                });

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    config.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.Access, p => { p.AddRequirements(new AccessRequirement()); });
                opt.AddPolicy(Policies.Create, p => { p.Requirements.Add(new CreateRequirement()); });
                opt.AddPolicy(Policies.Update, p => { p.Requirements.Add(new UpdateRequirement()); });
                opt.AddPolicy(Policies.Download, p => { p.Requirements.Add(new DownloadRequirement()); });
                opt.AddPolicy(Policies.Mail, p => { p.Requirements.Add(new MailRequirement()); });
                opt.AddPolicy(Policies.Wapp, p => { p.Requirements.Add(new WappRequirement()); });
            });

            services.AddTransient<IAuthorizationHandler, AccessRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, CreateRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, UpdateRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, DownloadRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, MailRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, WappRequirementHandler>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("WWW-Authenticate", "Content-Disposition").AllowAnyOrigin();
                });
            });

            services.AddScoped<ICatalogApplication<Delivery>, CatalogApplication<Delivery>>();
            services.AddScoped<ICatalogApplication<Area>, CatalogApplication<Area>>();
            services.AddScoped<ICatalogApplication<Bank>, CatalogApplication<Bank>>();
            services.AddScoped<ICatalogApplication<Provenance>, CatalogApplication<Provenance>>();
            services.AddScoped<ICatalogApplication<Format>, CatalogApplication<Format>>();
            services.AddScoped<ICatalogApplication<Clinic>, CatalogApplication<Clinic>>();
            services.AddScoped<ICatalogApplication<Department>, CatalogApplication<Department>>();
            services.AddScoped<ICatalogApplication<Method>, CatalogApplication<Method>>();
            services.AddScoped<ICatalogApplication<PaymentMethod>, CatalogApplication<PaymentMethod>>();
            services.AddScoped<ICatalogApplication<SampleType>, CatalogApplication<SampleType>>();
            services.AddScoped<ICatalogApplication<Field>, CatalogApplication<Field>>();
            services.AddScoped<ICatalogApplication<Units>, CatalogApplication<Units>>();
            services.AddScoped<ICatalogApplication<WorkList>, CatalogApplication<WorkList>>();
            services.AddScoped<ICatalogApplication<Equipos>, CatalogApplication<Equipos>>();
            services.AddScoped<ICatalogDescriptionApplication<InvoiceConcepts>, CatalogDescriptionApplication<InvoiceConcepts>>();
            services.AddScoped<ICatalogDescriptionApplication<UseOfCFDI>, CatalogDescriptionApplication<UseOfCFDI>>();
            services.AddScoped<ICatalogDescriptionApplication<Payment>, CatalogDescriptionApplication<Payment>>();
            services.AddScoped<ICatalogDescriptionApplication<Indicator>, CatalogDescriptionApplication<Indicator>>();
            services.AddScoped<IEquipmentMantainApplication, EquipmentMantainApplication>();
            services.AddScoped<IEquipmentApplication, EquipmentApplication>();

            services.AddScoped<IConfigurationApplication, ConfigurationApplication>();
            services.AddScoped<IAreaApplication, AreaApplication>();
            services.AddScoped<IDimensionApplication, DimensionApplication>();
            services.AddScoped<IReagentApplication, ReagentApplication>();
            services.AddScoped<IMedicsApplication, MedicsApplication>();
            services.AddScoped<IIndicationApplication, IndicationApplication>();
            services.AddScoped<ILocationApplication, LocationApplication>();
            services.AddScoped<IBranchApplication, BranchApplication>();
            services.AddScoped<ICompanyApplication, CompanyApplication>();
            services.AddScoped<IParameterApplication, ParameterApplication>();
            services.AddScoped<IMaquilaApplication, MaquilaApplication>();
            services.AddScoped<IStudyApplication, StudyApplication>();
            services.AddScoped<ITagApplication, TagApplication>();
            services.AddScoped<IPriceListApplication, PriceListApplication>();
            services.AddScoped<IPackApplication, PackApplication>();
            services.AddScoped<IPromotionApplication, PromotionApplication>();
            services.AddScoped<ILoyaltyApplication, LoyaltyApplication>();
            services.AddScoped<IRouteApplication, RouteApplication>();
            services.AddScoped<IBudgetApplication, BudgetApplication>();
            services.AddScoped<ISeriesApplication, SeriesApplication>();

            services.AddScoped<ICatalogRepository<Delivery>, CatalogRepository<Delivery>>();
            services.AddScoped<ICatalogRepository<Area>, CatalogRepository<Area>>();
            services.AddScoped<ICatalogRepository<InvoiceConcepts>, CatalogRepository<InvoiceConcepts>>();
            services.AddScoped<ICatalogRepository<Bank>, CatalogRepository<Bank>>();
            services.AddScoped<ICatalogRepository<Provenance>, CatalogRepository<Provenance>>();
            services.AddScoped<ICatalogRepository<Format>, CatalogRepository<Format>>();
            services.AddScoped<ICatalogRepository<Clinic>, CatalogRepository<Clinic>>();
            services.AddScoped<ICatalogRepository<Department>, CatalogRepository<Department>>();
            services.AddScoped<ICatalogRepository<Method>, CatalogRepository<Method>>();
            services.AddScoped<ICatalogRepository<Units>, CatalogRepository<Units>>();
            services.AddScoped<ICatalogRepository<PaymentMethod>, CatalogRepository<PaymentMethod>>();
            services.AddScoped<ICatalogRepository<SampleType>, CatalogRepository<SampleType>>();
            services.AddScoped<ICatalogRepository<Field>, CatalogRepository<Field>>();
            services.AddScoped<ICatalogRepository<WorkList>, CatalogRepository<WorkList>>();
            services.AddScoped<ICatalogRepository<Equipos>, CatalogRepository<Equipos>>();
            services.AddScoped<ICatalogRepository<UseOfCFDI>, CatalogRepository<UseOfCFDI>>();
            services.AddScoped<ICatalogRepository<Payment>, CatalogRepository<Payment>>();
            services.AddScoped<ICatalogRepository<Indicator>, CatalogRepository<Indicator>>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IDimensionRepository, DimensionRepository>();
            services.AddScoped<IReagentRepository, ReagentRepository>();
            services.AddScoped<IMedicsRepository, MedicsRepository>();
            services.AddScoped<IIndicationRepository, IndicationRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IMaquilaRepository, MaquilaRepository>();
            services.AddScoped<IStudyRepository, StudyRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPriceListRepository, PriceListRepository>();
            services.AddScoped<IPackRepository, PackRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<ILoyaltyRepository, LoyaltyRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IEquipmentMantainRepository, EquipmentMantainRepository>();
            services.AddScoped<IEquipmentApplication, EquipmentApplication>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service.Report v1");
                });
            }
            else if (env.IsEnvironment("QA"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/LabRamos/services/catalogo/swagger/v1/swagger.json", "Service.Report v1");
                });
            }

            app.UseCors("CorsPolicy");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(env.ContentRootPath, "wwwroot/images")),
                RequestPath = "/images",
                OnPrepareResponse = (context) =>
                {
                    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers["Pragma"] = "no-cache";
                    context.Context.Response.Headers["Expires"] = "-1";
                }
            });

            app.UseMiddleware<ErrorMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
