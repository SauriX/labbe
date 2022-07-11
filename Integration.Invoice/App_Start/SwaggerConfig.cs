using System.Web.Http;
using WebActivatorEx;
using Integration.Invoice;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Integration.Invoice
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Integration.Invoice");
                    })
                .EnableSwaggerUi(c =>
                    {
                    });
        }
    }
}
