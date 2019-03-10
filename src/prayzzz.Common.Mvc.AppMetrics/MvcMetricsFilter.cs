using System;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static prayzzz.Common.Mvc.AppMetrics.PrzMetricConstants;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public class MvcMetricsFilter : IAsyncResourceFilter
    {
        private readonly IMetrics _metrics;

        public MvcMetricsFilter(IMetrics metrics)
        {
            _metrics = metrics;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            try
            {
                var start = _metrics.Clock.Nanoseconds;
                await next.Invoke();
                var elapsed = _metrics.Clock.Nanoseconds - start;

                RecordRequestTime(context, elapsed);
            }
            catch (Exception)
            {
                _metrics.Measure.Counter.Increment(HttpRequestMetricsRegistry.UnhandledExceptionCount);

                throw;
            }
        }

        private void RecordRequestTime(ActionContext context, long elapsed)
        {
            var routeName = context.ActionDescriptor?.AttributeRouteInfo?.Template.ToLowerInvariant();
            if (routeName != null)
            {
                var tags = new MetricTags(new[] { HttpStatusCodeTag, MethodTag, RouteTag },
                                          new[] { context.HttpContext.Response.StatusCode.ToString(), context.HttpContext.Request.Method, routeName });

                _metrics.Provider.Timer.Instance(HttpRequestMetricsRegistry.RequestTimer, tags).Record(elapsed, TimeUnit.Nanoseconds);
            }
        }
    }
}