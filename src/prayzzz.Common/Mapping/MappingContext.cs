﻿using System.Collections.Generic;

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

        public MappingContext PutParam(string key, object value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key] = value;
            }

            _parameters.Add(key, value);

            return this;
        }

        public TParam GetParam<TParam>(string key) where TParam : class
        {
            object value;
            if (_parameters.TryGetValue(key, out value))
            {
                return value as TParam;
            }

            throw new InvalidContextException($"Requested parameter with name '{key}' missing.");
        }
    }
}