using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prayzzz.Common.Results;

namespace prayzzz.Common.Mvc.Json
{
    public class ResultConverter : JsonConverter
    {
        private static readonly Type ResultType = typeof(Result);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var result = (Result) value;

            var jObject = new JObject
            {
                new JProperty(nameof(Result.ErrorType), result.ErrorType),
                new JProperty(nameof(Result.Message), result.ToMessageString()),
                new JProperty(nameof(Result.IsError), result.IsError),
                new JProperty(nameof(Result.IsSuccess), result.IsSuccess)
            };

            jObject.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobject = (JObject) serializer.Deserialize(reader);

            var errorType = (ErrorType) jobject.GetValue(nameof(Result.ErrorType), StringComparison.OrdinalIgnoreCase).Value<int>();
            var message = jobject.GetValue(nameof(Result.Message), StringComparison.OrdinalIgnoreCase).Value<string>();

            return new Result(errorType, message);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == ResultType;
        }
    }
}