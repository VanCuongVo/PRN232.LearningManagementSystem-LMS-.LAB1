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
                await response.WriteAsync(
                    "<h1>No data</h1>");

                return;
            }

            var objectType = context.Object.GetType();

            var dataProperty =
                objectType.GetProperty("Data");

            if (dataProperty == null)
            {
                await response.WriteAsync(
                    "<h1>No Data property</h1>");

                return;
            }

            var data =
                dataProperty.GetValue(context.Object);

            if (data == null)
            {
                await response.WriteAsync(
                    "<h1>Empty data</h1>");

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

                        ul {
                            margin: 0;
                            padding-left: 20px;
                        }
                    </style>
                </head>

                <body>
            ");

            if (data is System.Collections.IEnumerable enumerable
                && data is not string)
            {
                var items =
                    enumerable.Cast<object>().ToList();

                if (items.Any())
                {
                    html.Append("<table>");

                    // =========================
                    // Dictionary
                    // =========================
                    if (items[0]
                        is Dictionary<string, object> dict)
                    {
                        html.Append("<tr>");

                        foreach (var key in dict.Keys)
                        {
                            html.Append(
                                $"<th>{key}</th>");
                        }

                        html.Append("</tr>");

                        foreach (var item in items)
                        {
                            var row =
                                item as Dictionary<string, object>;

                            html.Append("<tr>");

                            foreach (var value in row!.Values)
                            {
                                html.Append(
                                    $"<td>{FormatValue(value)}</td>");
                            }

                            html.Append("</tr>");
                        }
                    }

                    // =========================
                    // Normal Object
                    // =========================
                    else
                    {
                        var properties = items[0]
                            .GetType()
                            .GetProperties(
                                BindingFlags.Public |
                                BindingFlags.Instance)
                            .Where(p =>
                                p.GetIndexParameters().Length == 0)
                            .ToList();

                        // Header
                        html.Append("<tr>");

                        foreach (var prop in properties)
                        {
                            html.Append(
                                $"<th>{prop.Name}</th>");
                        }

                        html.Append("</tr>");

                        // Rows
                        foreach (var item in items)
                        {
                            html.Append("<tr>");

                            foreach (var prop in properties)
                            {
                                try
                                {
                                    var value =
                                        prop.GetValue(item);

                                    html.Append(
                                        $"<td>{FormatValue(value)}</td>");
                                }
                                catch
                                {
                                    html.Append("<td></td>");
                                }
                            }

                            html.Append("</tr>");
                        }
                    }

                    html.Append("</table>");
                }
            }

            html.Append("</body></html>");

            response.ContentType = "text/html";

            await response.WriteAsync(
                html.ToString());
        }

        private string FormatValue(object? value)
        {
            if (value == null)
                return "";

            // =========================
            // List
            // =========================
            if (value is System.Collections.IEnumerable list
                && value is not string)
            {
                var html = new StringBuilder();

                html.Append("<ul>");

                foreach (var item in list)
                {
                    if (item == null)
                        continue;

                    html.Append(
                        $"<li>{FormatValue(item)}</li>");
                }

                html.Append("</ul>");

                return html.ToString();
            }

            var type = value.GetType();

            // =========================
            // Primitive
            // =========================
            if (type.IsPrimitive
                || value is string
                || value is decimal
                || value is DateTime
                || value is Guid
                || value is Enum)
            {
                return value.ToString() ?? "";
            }

            // =========================
            // Complex Object
            // =========================
            var htmlObject = new StringBuilder();

            htmlObject.Append("<ul>");

            var props = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance)
                .Where(p =>
                    p.GetIndexParameters().Length == 0);

            foreach (var prop in props)
            {
                try
                {
                    htmlObject.Append(
                        $"<li><b>{prop.Name}</b>: {prop.GetValue(value)}</li>");
                }
                catch
                {
                }
            }

            htmlObject.Append("</ul>");

            return htmlObject.ToString();
        }
    }
}