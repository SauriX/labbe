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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.MedicalRecord.Application;
using Service.MedicalRecord.Application.IApplication;
using Service.MedicalRecord.Client;
using Service.MedicalRecord.Client.IClient;
using Service.MedicalRecord.Consumers;
using Service.MedicalRecord.Context;
using Service.MedicalRecord.Middleware;
using Service.MedicalRecord.Repository;
using Service.MedicalRecord.Repository.IRepository;
using Service.MedicalRecord.Requirements;
using Service.MedicalRecord.Settings;
using Service.MedicalRecord.Settings.ISettings;
using Service.MedicalRecord.Transactions;
using Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
namespace Service.MedicalRecord
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
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Scoped);

            services.AddScoped<ITransactionProvider, TransactionProvider>();

            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("Default"));

            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<RabbitMQSettings>(Configuration.GetSection(nameof(RabbitMQSettings)));
            services.AddSingleton<IRabbitMQSettings>(s => s.GetRequiredService<IOptions<RabbitMQSettings>>().Value);

            services.Configure<QueueNames>(Configuration.GetSection(nameof(QueueNames)));
            services.AddSingleton<IQueueNames>(s => s.GetRequiredService<IOptions<QueueNames>>().Value);

            services.AddScoped<IIdentityClient, IdentityClient>();
            services.AddScoped<ICatalogClient, CatalogClient>();
            services.AddScoped<IPdfClient, PdfClient>();
            services.AddScoped<IBillingClient, BillingClient>();

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

            services.AddHttpClient<ICatalogClient, CatalogClient>(client =>
            {
                var token = new HttpContextAccessor().HttpContext.Request.Headers["Authorization"].ToString();

                client.BaseAddress = new Uri(Configuration["ClientUrls:Catalog"]);

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

            services.AddHttpClient<IBillingClient, BillingClient>(client =>
            {
                var token = new HttpContextAccessor().HttpContext.Request.Headers["Authorization"].ToString();

                client.BaseAddress = new Uri(Configuration["ClientUrls:Billing"]);

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

                    configurator.Host(new Uri(rabbitMQSettings.Host), "MedicalRecord", c =>
                    {
                        c.ContinuationTimeout(TimeSpan.FromSeconds(20));
                        c.Username(rabbitMQSettings.Username);
                        c.Password(rabbitMQSettings.Password);
                    });

                    configurator.ReceiveEndpoint(queueNames.Branch, re =>
                    {
                        re.Consumer<BranchConsumer>(context);
                        re.DiscardFaultedMessages();
                    });

                    configurator.ReceiveEndpoint(queueNames.Company, re =>
                    {
                        re.Consumer<CompanyConsumer>(context);
                        re.DiscardFaultedMessages();
                    });

                    configurator.ReceiveEndpoint(queueNames.Medic, re =>
                    {
                        re.Consumer<MedicConsumer>(context);
                        re.DiscardFaultedMessages();
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.MedicalRecord", Version = "v1" });
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
                }).AddJsonOptions(config => config.JsonSerializerOptions.MaxDepth = 8);

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.Access, p =>
                {
                    p.RequireAuthenticatedUser();
                    p.AddRequirements(new AccessRequirement());
                });
                opt.AddPolicy(Policies.Create, p =>
                {
                    p.RequireAuthenticatedUser();
                    p.Requirements.Add(new CreateRequirement());
                });
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

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IMedicalRecordApplication, MedicalRecordApplication>();
            services.AddScoped<IQuotationApplication, QuotationApplication>();
            services.AddScoped<IRequestApplication, RequestApplication>();
            services.AddScoped<IReportApplication, ReportApplication>();
            services.AddScoped<IAppointmentApplication, AppointmentApplication>();
            services.AddScoped<ISamplingApplication, SamplingAplication>();
            services.AddScoped<IRequestedStudyApplication, RequestedStudyApplication>();
            services.AddScoped<IClinicResultsApplication, ClinicResultsApplication>();
            services.AddScoped<ITrackingOrderApplication, TrackingOrderApplication>();
            services.AddScoped<IRouteTrackingApplication, RouteTrackingApplication>();
            services.AddScoped<IShipmentTrackingApplication, ShipmentTrackingApplication>();
            services.AddScoped<IWorkListApplication, WorkListApplication>();
            services.AddScoped<IWeeClinicApplication, WeeClinicApplication>();
            services.AddScoped<IMassSearchApplication, MassSearchApplication>();
            services.AddScoped<IValidationApplication, ResultValidationApplication>();
            services.AddScoped<IRelaseResultApplication, RelaseResultApplication>();

            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IQuotationRepository, QuotationRepository>();
            services.AddScoped<IAppointmentResposiotry, AppointmentRepository>();
            services.AddScoped<ISamplingRepository, SamplingRepository>();
            services.AddScoped<IRequestedStudyRepository, RequestedStudyRepository>();
            services.AddScoped<IClinicResultsRepository, ClinicResultsRepository>();
            services.AddScoped<ITrackingOrderRepository, TrackingOrderRepository>();
            services.AddScoped<IRouteTrackingRepository, RouteTrackingRepository>();
            services.AddScoped<IShipmentTrackingRepository, ShipmentTrackingRepository>();
            services.AddScoped<IWorkListRepository, WorkListRepository>();
            services.AddScoped<IMassSearchRepository, MassSearchRepository>();
            services.AddScoped<IResultaValidationRepository, ResultValidationRepository>();
            services.AddScoped<IRelaseResultRepository, RelaseResultRepository>();
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
                    c.SwaggerEndpoint("/LabRamos/services/records/swagger/v1/swagger.json", "Service.Report v1");
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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(env.ContentRootPath, "wwwroot/layout/consent")),
                RequestPath = "/consents",
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(env.ContentRootPath, "wwwroot/temp")),
                RequestPath = "/temp",
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
