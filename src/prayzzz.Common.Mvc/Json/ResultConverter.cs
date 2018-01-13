using System;
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
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = (JObject) serializer.Deserialize(reader);

            var success = jsonObject.GetValue(nameof(Result.IsSuccess), StringComparison.OrdinalIgnoreCase);
            if (success != null && success.ToObject<bool>())
            {
                return jsonObject.ToObject<SuccessResult>();
            }
            else
            {
                return jsonObject.ToObject<ErrorResult>();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return ResultType == objectType;
        }
    }

    public class ResultOfTConverter : JsonConverter
    {
        private static readonly Type ResultOfTType;

        static ResultOfTConverter()
        {
            ResultOfTType = typeof(Result<>);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = (JObject) serializer.Deserialize(reader);

            var success = jsonObject.GetValue(nameof(Result.IsSuccess), StringComparison.OrdinalIgnoreCase);
            if (success != null && success.ToObject<bool>())
            {
                var type = typeof(SuccessResult<>).MakeGenericType(objectType.GenericTypeArguments);
                return jsonObject.ToObject(type);
            }
            else
            {
                var type = typeof(ErrorResult<>).MakeGenericType(objectType.GenericTypeArguments);
                return jsonObject.ToObject(type);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == ResultOfTType;
        }
    }
}