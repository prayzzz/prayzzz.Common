using System;
using Newtonsoft.Json.Serialization;
using prayzzz.Common.Results;

namespace prayzzz.Common.Mvc.Json
{
    public class ResultContractResolver : DefaultContractResolver
    {
        private static readonly Type ResultType;
        private static readonly Type ResultOfTType;
        private static readonly ResultConverter ResultConverter;
        private static readonly ResultOfTConverter ResultOfTConverter;

        static ResultContractResolver()
        {
            ResultType = typeof(Result);
            ResultOfTType = typeof(Result<>);
            ResultConverter = new ResultConverter();
            ResultOfTConverter = new ResultOfTConverter();
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == ResultOfTType)
            {
                // Result<T>
                contract.Converter = ResultOfTConverter;
            }
            else if (objectType == ResultType)
            {
                // Result
                contract.Converter = ResultConverter;
            }

            return contract;
        }
    }
}