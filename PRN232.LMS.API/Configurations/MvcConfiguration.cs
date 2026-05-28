using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRN232.LMS.API.Formatters;

namespace PRN232.LMS.API.Configurations
{
    public static class MvcConfiguration
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.ReturnHttpNotAcceptable = true;

                options.OutputFormatters.Add(
                    new CsvOutputFormatter());

                options.OutputFormatters.Add(
                    new HtmlOutputFormatter());

                options.OutputFormatters.Add(
                    new XmlOutputFormatter());
            });

            return services;
        }
    }
}