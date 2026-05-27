using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace PRN232.LMS.API.Formatters
{
    public class HtmlOutputFormatter : TextOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add(
                MediaTypeHeaderValue.Parse("text/html"));

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
                await response.WriteAsync("<h1>No data</h1>");
                return;
            }

            var objectType = context.Object.GetType();

            var dataProperty = objectType.GetProperty("Data");

            if (dataProperty == null)
            {
                await response.WriteAsync("<h1>No Data property</h1>");
                return;
            }

            var data = dataProperty.GetValue(context.Object);

            if (data == null)
            {
                await response.WriteAsync("<h1>Empty data</h1>");
                return;
            }

            var html = new StringBuilder();

            html.Append(@"
                <html>
                <head>
                    <title>Data</title>
                    <style>
                        table {
                            border-collapse: collapse;
                            width: 100%;
                        }

                        th, td {
                            border: 1px solid black;
                            padding: 8px;
                        }

                        th {
                            background-color: #f2f2f2;
                        }
                    </style>
                </head>
                <body>
            ");

            if (data is System.Collections.IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().ToList();

                if (items.Any())
                {
                    var properties = items[0]
                        .GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    html.Append("<table>");

                    // Header
                    html.Append("<tr>");

                    foreach (var prop in properties)
                    {
                        html.Append($"<th>{prop.Name}</th>");
                    }

                    html.Append("</tr>");

                    // Rows
                    foreach (var item in items)
                    {
                        html.Append("<tr>");

                        foreach (var prop in properties)
                        {
                            html.Append(
                                $"<td>{prop.GetValue(item)}</td>");
                        }

                        html.Append("</tr>");
                    }

                    html.Append("</table>");
                }
            }
            else
            {
                html.Append($"<p>{data}</p>");
            }

            html.Append("</body></html>");

            await response.WriteAsync(html.ToString());
        }
    }
}