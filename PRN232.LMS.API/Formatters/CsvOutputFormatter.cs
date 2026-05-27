using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace PRN232.LMS.API.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(
                MediaTypeHeaderValue.Parse("text/csv"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            return true;
        }

        public override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;

            if (context.Object == null)
            {
                await response.WriteAsync("No data");
                return;
            }

            var objectType = context.Object.GetType();

            // Lấy property Data
            var dataProperty = objectType.GetProperty("Data");

            if (dataProperty == null)
            {
                await response.WriteAsync("No Data property");
                return;
            }

            var data = dataProperty.GetValue(context.Object);

            if (data == null)
            {
                await response.WriteAsync("Empty data");
                return;
            }

            // IEnumerable
            if (data is System.Collections.IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToList();

                if (!items.Any())
                {
                    await response.WriteAsync("No rows");
                    return;
                }

                var properties = items[0]
                    .GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var builder = new StringBuilder();

                // Header
                builder.AppendLine(
                    string.Join(",", properties.Select(p => p.Name)));

                // Rows
                foreach (var item in items)
                {
                    var values = properties.Select(p =>
                    {
                        var value = p.GetValue(item);

                        return value?.ToString()?.Replace(",", " ")
                               ?? "";
                    });

                    builder.AppendLine(string.Join(",", values));
                }

                await response.WriteAsync(builder.ToString());

                return;
            }

            await response.WriteAsync(data.ToString());
        }
    }
}