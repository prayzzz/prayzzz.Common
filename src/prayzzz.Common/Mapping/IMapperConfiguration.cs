using System;

namespace prayzzz.Common.Mapping
{
    public interface IMapperConfiguration
    {
        void Configure<TSource, TTarget>(Func<TSource, TTarget, MappingContext, TTarget> func) where TTarget : class;

        void Configure<TSource, TTarget>(Func<TSource, MappingContext, TTarget> func) where TTarget : class;
    }
}