using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using prayzzz.Common.Results;

namespace prayzzz.Common.Mvc.Json
{
    public class ResultContractResolver : DefaultContractResolver
    {
        private static readonly Type ResultType;
        private static readonly ResultConverter ResultConverter;

        static ResultContractResolver()
        {
            ResultType = typeof(Result);
            ResultConverter = new ResultConverter();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (ResultType.IsAssignableFrom(property.DeclaringType) && property.PropertyName == nameof(Result<object>.Data))
            {
                property.ShouldSerialize = o => false;
            }

            return property;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (ResultType.IsAssignableFrom(objectType))
            {
                contract.Converter = ResultConverter;
            }

            return contract;
        }
    }
}