using System.Collections.Generic;

namespace prayzzz.Common.Mapping
{
    public class MappingContext
    {
        private readonly Dictionary<string, object> _parameters;

        public MappingContext()
        {
            _parameters = new Dictionary<string, object>();
        }

        public IMapper Mapper { get; set; }

        public TParam GetParam<TParam>(string key) where TParam : class
        {
            if (_parameters.TryGetValue(key, out var value))
            {
                return value as TParam;
            }

            throw new InvalidContextException($"Requested parameter with name '{key}' missing.");
        }

        public MappingContext PutParam(string key, object value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key] = value;
            }

            _parameters.Add(key, value);

            return this;
        }
    }
}