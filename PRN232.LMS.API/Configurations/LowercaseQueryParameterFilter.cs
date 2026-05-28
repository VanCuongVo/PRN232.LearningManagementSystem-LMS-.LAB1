using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PRN232.LMS.API.Configurations
{
    public class LowercaseQueryParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation?.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                if (parameter?.Name == null) continue;

                parameter.Name = ToCamelCase(parameter.Name);
            }
        }

        private static string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

    }
}