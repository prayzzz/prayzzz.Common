using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prayzzz.Common.Results;

namespace prayzzz.Common.Mvc.Json
{
    public class ResultConverter : JsonConverter
    {
        private static readonly Type ResultType;

        static ResultConverter()
        {
            ResultType = typeof(Result);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType.IsConstructedGenericType)
            {
                throw new JsonSerializationException("Cannot deserialize data results");
            }

            var jsonObject = (JObject) serializer.Deserialize(reader);

            var isSuccess = jsonObject.GetValue(nameof(Result.IsSuccess), StringComparison.OrdinalIgnoreCase).Value<bool>();
            var message = jsonObject.GetValue(nameof(Result.Message), StringComparison.OrdinalIgnoreCase).Value<string>();
            var messageArgs = jsonObject.GetValue(nameof(Result.MessageArgs), StringComparison.OrdinalIgnoreCase).Values<string>().ToArray<object>();
            var errorType = (ErrorType) jsonObject.GetValue(nameof(Result.ErrorType), StringComparison.OrdinalIgnoreCase).Value<int>();

            if (!isSuccess)
            {
                var exception = jsonObject.GetValue(nameof(Result.Exception), StringComparison.OrdinalIgnoreCase).ToObject<Exception>();

                if (exception != null)
                {
                    return new ErrorResult(exception, message, messageArgs);
                }

                return new ErrorResult(errorType, message, messageArgs);
            }

            return new SuccessResult(message, messageArgs);
        }

        public override bool CanConvert(Type objectType)
        {
            return ResultType.IsAssignableFrom(objectType);
        }
    }
}