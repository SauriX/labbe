using FluentValidation.AspNetCore;
using Identidad.Api.Infraestructure.Repository;
using Identidad.Api.Infraestructure.Repository.IRepository;
using Identidad.Api.Infraestructure.Services;
using Identidad.Api.Infraestructure.Services.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service.Catalog.Application;
using Service.Catalog.Application.IApplication;
using Service.Catalog.Context;
using Service.Catalog.Domain.Catalog;
using Service.Catalog.Domain.Indication;
using Service.Catalog.Middleware;
using Service.Catalog.Repository;
using Service.Catalog.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            }, ServiceLifetime.Scoped);

            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("Default"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Catalog", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("WWW-Authenticate").AllowAnyOrigin();
                });
            });

            services.AddControllers()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    config.ValidatorOptions.LanguageManager.Culture = new CultureInfo("es");
                });

            services.AddScoped<ICatalogApplication<Delivery>, CatalogApplication<Delivery>>();
            services.AddScoped<ICatalogApplication<Area>, CatalogApplication<Area>>();
            services.AddScoped<ICatalogApplication<Bank>, CatalogApplication<Bank>>();
            services.AddScoped<ICatalogApplication<Clinic>, CatalogApplication<Clinic>>();
            services.AddScoped<ICatalogApplication<Department>, CatalogApplication<Department>>();
            services.AddScoped<ICatalogApplication<Method>, CatalogApplication<Method>>();
            services.AddScoped<ICatalogApplication<PaymentMethod>, CatalogApplication<PaymentMethod>>();
            services.AddScoped<ICatalogApplication<SampleType>, CatalogApplication<SampleType>>();
            services.AddScoped<ICatalogApplication<Field>, CatalogApplication<Field>>();
            services.AddScoped<ICatalogApplication<WorkList>, CatalogApplication<WorkList>>();
            services.AddScoped<ICatalogDescriptionApplication<UseOfCFDI>, CatalogDescriptionApplication<UseOfCFDI>>();
            services.AddScoped<ICatalogDescriptionApplication<Payment>, CatalogDescriptionApplication<Payment>>();
            services.AddScoped<ICatalogDescriptionApplication<Indicator>, CatalogDescriptionApplication<Indicator>>();
            services.AddScoped<IAreaApplication, AreaApplication>();
            services.AddScoped<IDimensionApplication, DimensionApplication>();
            services.AddScoped<IReagentApplication, ReagentApplication>();
            services.AddScoped<IMedicsApplication, MedicsApplication>();
            services.AddScoped<IIndicationApplication, IndicationApplication>();

            services.AddScoped<ICatalogRepository<Delivery>, CatalogRepository<Delivery>>();
            services.AddScoped<ICatalogRepository<Area>, CatalogRepository<Area>>();
            services.AddScoped<ICatalogRepository<Bank>, CatalogRepository<Bank>>();
            services.AddScoped<ICatalogRepository<Clinic>, CatalogRepository<Clinic>>();
            services.AddScoped<ICatalogRepository<Department>, CatalogRepository<Department>>();
            services.AddScoped<ICatalogRepository<Method>, CatalogRepository<Method>>();
            services.AddScoped<ICatalogRepository<PaymentMethod>, CatalogRepository<PaymentMethod>>();
            services.AddScoped<ICatalogRepository<SampleType>, CatalogRepository<SampleType>>();
            services.AddScoped<ICatalogRepository<Field>, CatalogRepository<Field>>();
            services.AddScoped<ICatalogRepository<WorkList>, CatalogRepository<WorkList>>();
            services.AddScoped<ICatalogRepository<UseOfCFDI>, CatalogRepository<UseOfCFDI>>();
            services.AddScoped<ICatalogRepository<Payment>, CatalogRepository<Payment>>();
            services.AddScoped<ICatalogRepository<Indicator>, CatalogRepository<Indicator>>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IDimensionRepository, DimensionRepository>();
            services.AddScoped<IReagentRepository, ReagentRepository>();
            services.AddScoped<IMedicsRepository, MedicsRepository>();
            services.AddScoped<IIndicationRepository, IndicationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service.Catalog v1"));
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
