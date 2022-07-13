using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service.Identity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Identity.Repository;
using Service.Identity.Repository.IRepository;
using Service.Identity.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.Dictionary;
using Service.Identity.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Globalization;
using Service.Identity.Application.IApplication;
using Service.Identity.Application;
using Service.Identity.Middleware;

namespace Service.Identity
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

            services.AddHealthChecks()
               .AddSqlServer(Configuration.GetConnectionString("Default"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Identity", Version = "v1" });

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

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.Access, p => p.RequireAuthenticatedUser().Requirements.Add(new AccessRequirement()));
                opt.AddPolicy(Policies.Create, p => p.RequireAuthenticatedUser().Requirements.Add(new CreateRequirement()));
                opt.AddPolicy(Policies.Update, p => p.RequireAuthenticatedUser().Requirements.Add(new UpdateRequirement()));
                opt.AddPolicy(Policies.Download, p => p.RequireAuthenticatedUser().Requirements.Add(new DownloadRequirement()));
                opt.AddPolicy(Policies.Mail, p => p.RequireAuthenticatedUser().Requirements.Add(new MailRequirement()));
                opt.AddPolicy(Policies.Wapp, p => p.RequireAuthenticatedUser().Requirements.Add(new WappRequirement()));
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
                    policy.AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("WWW-Authenticate").AllowAnyOrigin();
                });
            });

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    config.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");
                });

            services.AddScoped<IProfileApplication, ProfileApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IRoleApplication, RoleApplication>();

            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
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
                    c.SwaggerEndpoint("/LabRamos/services/identity/swagger/v1/swagger.json", "Service.Report v1");
                });
            }

            app.UseCors("CorsPolicy");

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
