namespace prayzzz.Common.Mapping
{
    public interface IMapper
    {
        TTarget Map<TTarget>(object source, MappingContext context) where TTarget : class;

        TTarget Map<TTarget>(object source, TTarget target, MappingContext context) where TTarget : class;

        TTarget Map<TTarget>(object source) where TTarget : class;

        TTarget Map<TTarget>(object source, TTarget target) where TTarget : class;
    }
}