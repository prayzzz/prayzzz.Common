using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using prayzzz.Common.Results;

namespace prayzzz.Common.Mvc.Json
{
    public class ResultConverter : JsonConverter
    {
        private readonly NamingStrategy _namingStrategy;
        private readonly ConcurrentDictionary<string, string> _propertyNames;

        public ResultConverter() : this(new DefaultNamingStrategy())
        {
        }

        public ResultConverter(NamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy ?? new DefaultNamingStrategy();
            _propertyNames = new ConcurrentDictionary<string, string>();
        }

        private static readonly Type ResultType = typeof(Result);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var result = (Result) value;

            var jObject = new JObject
            {
                new JProperty(GetPropertyName(nameof(Result.ErrorType)), result.ErrorType),
                new JProperty(GetPropertyName(nameof(Result.Message)), result.ToMessageString()),
                new JProperty(GetPropertyName(nameof(Result.IsError)), result.IsError),
                new JProperty(GetPropertyName(nameof(Result.IsSuccess)), result.IsSuccess)
            };

            jObject.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobject = (JObject) serializer.Deserialize(reader);

            var errorType = (ErrorType) jobject.GetValue(GetPropertyName(nameof(Result.ErrorType)), StringComparison.OrdinalIgnoreCase).Value<int>();
            var message = jobject.GetValue(GetPropertyName(nameof(Result.Message)), StringComparison.OrdinalIgnoreCase).Value<string>();

            return new Result(errorType, message);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == ResultType;
        }

        private string GetPropertyName(string name)
        {
            return _propertyNames.GetOrAdd(name, n => _namingStrategy.GetPropertyName(n, false));
        }
    }
}