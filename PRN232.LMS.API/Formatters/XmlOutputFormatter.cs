using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace PRN232.LMS.API.Formatters
{
    public class XmlOutputFormatter : TextOutputFormatter
    {
        public XmlOutputFormatter()
        {
            SupportedMediaTypes.Add(
                MediaTypeHeaderValue.Parse(
                    "application/xml"));

            SupportedEncodings.Add(
                Encoding.UTF8);

            SupportedEncodings.Add(
                Encoding.Unicode);
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
                    "<message>No data</message>");

                return;
            }

            var objectType =
                context.Object.GetType();

            var dataProperty =
                objectType.GetProperty("Data");

            if (dataProperty == null)
            {
                await response.WriteAsync(
                    "<message>No Data property</message>");

                return;
            }

            var data =
                dataProperty.GetValue(context.Object);

            if (data == null)
            {
                await response.WriteAsync(
                    "<message>Empty data</message>");

                return;
            }

            var root =
                new XElement("Items");

            if (data is System.Collections.IEnumerable enumerable
                && data is not string)
            {
                foreach (var item in enumerable)
                {
                    if (item == null)
                        continue;

                    var itemElement =
                        new XElement("Item");

                    // =========================
                    // Dictionary
                    // =========================
                    if (item
                        is Dictionary<string, object> dict)
                    {
                        foreach (var pair in dict)
                        {
                            AddXmlValue(
                                itemElement,
                                pair.Key,
                                pair.Value);
                        }
                    }

                    // =========================
                    // Normal Object
                    // =========================
                    else
                    {
                        var properties = item.GetType()
                            .GetProperties(
                                BindingFlags.Public |
                                BindingFlags.Instance);

                        foreach (var property in properties)
                        {
                            if (property
                                .GetIndexParameters()
                                .Length > 0)
                            {
                                continue;
                            }

                            try
                            {
                                var value =
                                    property.GetValue(item);

                                AddXmlValue(
                                    itemElement,
                                    property.Name,
                                    value);
                            }
                            catch
                            {
                            }
                        }
                    }

                    root.Add(itemElement);
                }
            }

            var document =
                new XDocument(root);

            response.ContentType =
                "application/xml";

            await response.WriteAsync(
                document.ToString());
        }

        private void AddXmlValue(
            XElement parent,
            string propertyName,
            object? value)
        {
            if (value == null)
            {
                parent.Add(
                    new XElement(
                        propertyName,
                        ""));

                return;
            }

            // =========================
            // List
            // =========================
            if (value is System.Collections.IEnumerable list
                && value is not string)
            {
                var listElement =
                    new XElement(propertyName);

                foreach (var child in list)
                {
                    if (child == null)
                        continue;

                    var childElement =
                        new XElement("Item");

                    var childProps = child.GetType()
                        .GetProperties(
                            BindingFlags.Public |
                            BindingFlags.Instance);

                    foreach (var childProp in childProps)
                    {
                        if (childProp
                            .GetIndexParameters()
                            .Length > 0)
                        {
                            continue;
                        }

                        try
                        {
                            var childValue =
                                childProp.GetValue(child);

                            AddXmlValue(
                                childElement,
                                childProp.Name,
                                childValue);
                        }
                        catch
                        {
                        }
                    }

                    listElement.Add(childElement);
                }

                parent.Add(listElement);

                return;
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
                parent.Add(
                    new XElement(
                        propertyName,
                        value));

                return;
            }
            // =========================
            // Complex Object
            // =========================
            var objectElement =
                new XElement(propertyName);

            var properties = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (property
                    .GetIndexParameters()
                    .Length > 0)
                {
                    continue;
                }

                try
                {
                    var childValue =
                        property.GetValue(value);

                    AddXmlValue(
                        objectElement,
                        property.Name,
                        childValue);
                }
                catch
                {
                }
            }

            parent.Add(objectElement);
        }
    }
}