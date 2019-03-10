using Microsoft.Extensions.DependencyInjection;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public static class MvcBuilderExtensions
    {
        public static void AddPrzMetrics(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(o => { o.Filters.Add(typeof(MvcMetricsFilter)); });
        }
    }
}