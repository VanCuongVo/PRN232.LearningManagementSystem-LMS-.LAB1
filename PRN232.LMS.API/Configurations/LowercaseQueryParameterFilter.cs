using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PRN232.LMS.API.Configurations
{
    public class LowercaseQueryParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation?.Parameters == null || operation.Parameters.Count == 0)
                return;

            for (int i = 0; i < operation.Parameters.Count; i++)
            {
                var parameter = operation.Parameters[i];
                if (parameter == null || string.IsNullOrEmpty(parameter.Name))
                    continue;

                var name = parameter.Name;
                if (name.Length == 0)
                    continue;

                var lower = char.ToLowerInvariant(name[0]) + (name.Length > 1 ? name.Substring(1) : string.Empty);
                if (name == lower)
                    continue;

                var newParam = new OpenApiParameter
                {
                    Name = lower,
                    In = parameter.In,
                    Description = parameter.Description,
                    Required = parameter.Required,
                    Schema = parameter.Schema,
                    Example = parameter.Example,
                    Examples = parameter.Examples,
                    Extensions = parameter.Extensions,
                    Style = parameter.Style,
                    AllowEmptyValue = parameter.AllowEmptyValue,
                    Deprecated = parameter.Deprecated,
                    Content = parameter.Content
                };

                operation.Parameters[i] = newParam;
            }
        }
    }
}