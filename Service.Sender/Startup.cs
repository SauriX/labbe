using MassTransit;
using MassTransit.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Sender.Consumers;
using Service.Sender.Consumers.Error;
using Service.Sender.Context;
using Service.Sender.Middleware;
using Service.Sender.Repository;
using Service.Sender.Repository.IRepository;
using Service.Sender.Service;
using Service.Sender.Service.IService;
using Service.Sender.Settings;
using Service.Sender.Settings.Interfaces;
using Service.Sender.SignalR;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Sender
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

            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.AddSignalRHub<NotificationHub>();

                x.UsingRabbitMq((context, configurator) =>
                {
                    var rabbitMQSettings = Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    var queueNames = Configuration.GetSection(nameof(QueueNames)).Get<QueueNames>();

                    configurator.Host(new Uri(rabbitMQSettings.Host), "Sender", c =>
                    {
                        c.ContinuationTimeout(TimeSpan.FromSeconds(20));
                        c.Username(rabbitMQSettings.Username);
                        c.Password(rabbitMQSettings.Password);
                    });

                    configurator.ReceiveEndpoint(queueNames.Email, re =>
                    {
                        re.Consumer<EmailConsumer>(context);
                        re.DiscardFaultedMessages();
                    });

                    configurator.ReceiveEndpoint(queueNames.EmailFault, re =>
                    {
                        re.Consumer<EmailErrorConsumer>(context);
                    });           
                    
                    configurator.ReceiveEndpoint(queueNames.Whatsapp, re =>
                    {
                        re.Consumer<WhatsappConsumer>(context);
                        re.DiscardFaultedMessages();
                    });

                    configurator.ReceiveEndpoint(queueNames.WhatsappFault, re =>
                    {
                        re.Consumer<WhatsappErrorConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(queueNames.EmailConfiguration, re =>
                    {
                        re.Consumer<EmailConfigurationConsumer>(context);
                        re.DiscardFaultedMessages();
                    });

                    configurator.ReceiveEndpoint(queueNames.EmailConfigurationFault, re =>
                    {
                        re.Consumer<EmailConfigurationErrorConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(queueNames.Notification, re =>
                    {
                        re.Consumer<NotificationConsumer>(context);
                        re.DiscardFaultedMessages();
                    });

                    configurator.ReceiveEndpoint(queueNames.NotificationFault, re =>
                    {
                        re.Consumer<NotificationErrorConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Sender", Version = "v1" });
            });

            services.Configure<KeySettings>(Configuration.GetSection(nameof(KeySettings)));
            services.AddSingleton<IKeySettings>(fs => fs.GetRequiredService<IOptions<KeySettings>>().Value);

            services.Configure<UrlSettings>(Configuration.GetSection(nameof(UrlSettings)));
            services.AddSingleton<IUrlSettings>(fs => fs.GetRequiredService<IOptions<UrlSettings>>().Value);

            services.Configure<UrlLocalSettings>(Configuration.GetSection(nameof(UrlLocalSettings)));
            services.AddSingleton<IUrlLocalSettings>(fs => fs.GetRequiredService<IOptions<UrlLocalSettings>>().Value);    
            
            services.Configure<EmailTemplateSettings>(Configuration.GetSection(nameof(EmailTemplateSettings)));
            services.AddSingleton<IEmailTemplateSettings>(fs => fs.GetRequiredService<IOptions<EmailTemplateSettings>>().Value);

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
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notification"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSignalR();

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("WWW-Authenticate", "Content-Disposition").AllowAnyOrigin();
                });
            });

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IWhatsappService, WhatsappService>();
            services.AddScoped<IEmailConfigurationService, EmailConfigurationService>();

            services.AddScoped<IEmailConfigurationRepository, EmailConfigurationRepository>();
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
                    c.SwaggerEndpoint("/LabRamos/services/sender/swagger/v1/swagger.json", "Service.Report v1");
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
                endpoints.MapHub<NotificationHub>("/notification");
            });
        }
    }
}
