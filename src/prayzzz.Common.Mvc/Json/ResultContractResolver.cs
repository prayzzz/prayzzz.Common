using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using prayzzz.Common.Results;

namespace prayzzz.Common.Mvc.Json
{
    public class ResultContractResolver : DefaultContractResolver
    {
        private readonly Type _resultType;
        private readonly ResultConverter _resultConverter;

        public ResultContractResolver()
        {
            _resultType = typeof(Result);
            _resultConverter = new ResultConverter(NamingStrategy);
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (_resultType.IsAssignableFrom(objectType))
            {
                contract.Converter = _resultConverter;
            }

            return contract;
        }
    }
}