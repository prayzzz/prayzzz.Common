using System;

namespace prayzzz.Common.Mapping
{
    public interface IMapperConfiguration
    {
        void Configure<TSource, TTarget>(Func<TSource, TTarget, MappingCtx, TTarget> func) where TTarget : class;

        void Configure<TSource, TTarget>(Func<TSource, MappingCtx, TTarget> func) where TTarget : class;
    }

    public interface IMapper
    {
        TTarget Map<TTarget>(object source, MappingCtx context) where TTarget : class;

        TTarget Map<TTarget>(object source, TTarget target, MappingCtx context) where TTarget : class;

        TTarget Map<TTarget>(object source) where TTarget : class;

        TTarget Map<TTarget>(object source, TTarget target) where TTarget : class;
    }
}