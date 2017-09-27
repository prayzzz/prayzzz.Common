using System;
using System.Collections.Generic;

namespace prayzzz.Common.Mapping
{
    public class Mapper : IMapperConfiguration, IMapper
    {
        private readonly Dictionary<TypeTuple, MappingRule> _rules;

        public Mapper(IEnumerable<IDboMapper> mappers)
        {
            _rules = new Dictionary<TypeTuple, MappingRule>();

            foreach (var dboMapper in mappers)
            {
                dboMapper.Configure(this);
            }
        }

        public TTarget Map<TTarget>(object source, MappingContext context) where TTarget : class
        {
            if (source == null)
            {
                return null;
            }

            var sourceType = source.GetType();
            var targetType = typeof(TTarget);

            var tuple = new TypeTuple(sourceType, targetType);

            if (!_rules.TryGetValue(tuple, out var rule))
            {
                throw new MissingMethodException($"Mapping from {sourceType.FullName} to {targetType.FullName} not configured.");
            }

            context.Mapper = this;

            if (rule.Map != null)
            {
                return rule.Map.DynamicInvoke(source, context) as TTarget;
            }

            if (rule.MapWithTarget != null)
            {
                var target = Activator.CreateInstance<TTarget>();
                return rule.MapWithTarget.DynamicInvoke(source, target, context) as TTarget;
            }

            throw new MissingMethodException("No mapping method found");
        }

        public TTarget Map<TTarget>(object source, TTarget target, MappingContext context) where TTarget : class
        {
            if (source == null)
            {
                return null;
            }

            var sourceType = source.GetType();
            var targetType = typeof(TTarget);

            var tuple = new TypeTuple(sourceType, targetType);

            if (!_rules.TryGetValue(tuple, out var rule))
            {
                throw new MissingMethodException($"Mapping from {sourceType.FullName} to {targetType.FullName} not configured.");
            }

            context.Mapper = this;

            if (rule.MapWithTarget != null)
            {
                return rule.MapWithTarget.DynamicInvoke(source, target, context) as TTarget;
            }

            if (rule.Map != null)
            {
                return rule.Map.DynamicInvoke(source, context) as TTarget;
            }

            throw new MissingMethodException("No mapping method found");
        }

        public TTarget Map<TTarget>(object source) where TTarget : class
        {
            return Map<TTarget>(source, new MappingContext());
        }

        public TTarget Map<TTarget>(object source, TTarget target) where TTarget : class
        {
            return Map(source, target, new MappingContext());
        }

        public void Configure<TSource, TTarget>(Func<TSource, TTarget, MappingContext, TTarget> func) where TTarget : class
        {
            var tuple = new TypeTuple(typeof(TSource), typeof(TTarget));

            if (!_rules.TryGetValue(tuple, out var rule))
            {
                rule = new MappingRule();
                _rules.Add(tuple, rule);
            }

            rule.MapWithTarget = func;
        }

        public void Configure<TSource, TTarget>(Func<TSource, MappingContext, TTarget> func) where TTarget : class
        {
            var tuple = new TypeTuple(typeof(TSource), typeof(TTarget));

            if (!_rules.TryGetValue(tuple, out var rule))
            {
                rule = new MappingRule();
                _rules.Add(tuple, rule);
            }

            rule.Map = func;
        }

        private class MappingRule
        {
            public Delegate Map { get; set; }

            public Delegate MapWithTarget { get; set; }
        }

        private class TypeTuple : IEquatable<TypeTuple>
        {
            public TypeTuple(Type source, Type target)
            {
                Source = source.TypeHandle;
                Target = target.TypeHandle;
            }

            public RuntimeTypeHandle Source { get; }

            public RuntimeTypeHandle Target { get; }

            public bool Equals(TypeTuple other)
            {
                if (other == null)
                {
                    return false;
                }

                return Source.Equals(other.Source) && Target.Equals(other.Target);
            }

            public override bool Equals(object obj)
            {
                var p = obj as TypeTuple;
                if (p == null)
                {
                    return false;
                }

                return Source.Equals(p.Source) && Target.Equals(p.Target);
            }

            public override int GetHashCode()
            {
                return Source.GetHashCode() ^ Target.GetHashCode();
            }
        }
    }
}