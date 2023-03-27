using System.Web.Http;
using WebActivatorEx;
using GTI_WebApi;
using Swashbuckle.Application;
using System.Configuration;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace GTI_WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            // if key in config does not exist we set it to true
            // you can play to make it always false ***
            bool isSwaggerEnabled = ConfigurationManager.AppSettings["IsSwaggerEnabled"] != null ?
                bool.Parse(ConfigurationManager.AppSettings["IsSwaggerEnabled"]) : true;

            // if swagger is not enabled we don't even bother to config it,
            // therefor will be disabled, and you don't have to worry about it.
            if (!isSwaggerEnabled)
                return;

            GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "GTI WebApi"); })
                .EnableSwaggerUi(c =>  {  });
        }
    }
}
