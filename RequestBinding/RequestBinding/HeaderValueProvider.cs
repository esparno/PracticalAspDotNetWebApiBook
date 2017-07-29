using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Net.Http.Headers;
using System.Web.Http.ValueProviders;

namespace RequestBinding
{
    public class HeaderValueProvider : IValueProvider
    {
        private readonly HttpRequestHeaders headers;
        private Func<KeyValuePair<string, IEnumerable<string>>, string, bool> predicate =
            (header, key) =>
            {
                return header.Key.Replace("-", String.Empty).Equals(key, StringComparison.OrdinalIgnoreCase);
            };
        public HeaderValueProvider (HttpRequestHeaders headers)
        {
            this.headers = headers;
        }
        public bool ContainsPrefix(string prefix)
        {
            return headers.Any(h => predicate(h, prefix));
        }
        public ValueProviderResult GetValue(string key)
        {
            var header = headers.FirstOrDefault(h => predicate(h, key));
            if (!String.IsNullOrEmpty(header.Key))
            {
                key = header.Key;
                var values = headers.GetValues(key);
                if(values.Count() > 1)
                {
                    return new ValueProviderResult(values, null, CultureInfo.CurrentCulture);
                }
                else
                {
                    string value = values.First();
                    values = value.Split(',').Select(x => x.Trim()).ToArray();
                    if (values.Count() > 1)
                    {
                        return new ValueProviderResult(values, null, CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        return new ValueProviderResult(value, value, CultureInfo.CurrentCulture);
                    }
                }
            }
        return null;
        }
    }
}