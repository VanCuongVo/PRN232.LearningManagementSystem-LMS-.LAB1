using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PRN232.LMS.API.Configurations
{
    public class ProducesResponseTypeOperationFilter : IOperationFilter
    {
        public void Apply(
            OpenApiOperation operation,
            OperationFilterContext context)
        {
            foreach (var response in operation.Responses)
            {
                response.Value.Content.Clear();

                response.Value.Content.Add(
                    "application/json",
                    new OpenApiMediaType());

                response.Value.Content.Add(
                    "application/xml",
                    new OpenApiMediaType());

                response.Value.Content.Add(
                    "text/csv",
                    new OpenApiMediaType());

                response.Value.Content.Add(
                    "text/html",
                    new OpenApiMediaType());
            }
        }
    }
}