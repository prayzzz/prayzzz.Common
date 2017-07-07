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

            var jsonObject = (JObject)serializer.Deserialize(reader);

            var isSuccess = TryGetValue<bool>(jsonObject, nameof(Result.IsSuccess));
            var message = TryGetValue<string>(jsonObject, nameof(Result.Message));
            var errorType = (ErrorType)TryGetValue<int>(jsonObject, nameof(Result.ErrorType));

            var messageArgs = Array.Empty<object>();
            if (jsonObject.TryGetValue(nameof(Result.MessageArgs), StringComparison.OrdinalIgnoreCase, out var value))
            {
                messageArgs = value.Values<string>().ToArray<object>();
            }

            if (isSuccess)
            {
                return new SuccessResult(message, messageArgs);
            }

            var exception = TryGetValue<Exception>(jsonObject, nameof(Result.Exception));
            if (exception != null)
            {
                return new ErrorResult(exception, message, messageArgs);
            }

            return new ErrorResult(errorType, message, messageArgs);
        }

        public override bool CanConvert(Type objectType)
        {
            return ResultType.IsAssignableFrom(objectType);
        }

        private static T TryGetValue<T>(JObject obj, string name)
        {
            if (obj.TryGetValue(name, StringComparison.OrdinalIgnoreCase, out var value))
            {
                return value.Value<T>();
            }

            return default(T);
        }
    }
}