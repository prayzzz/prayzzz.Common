using System;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.AspNetCore.Http;
using static prayzzz.Common.Mvc.AppMetrics.PrzMetricConstants;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public class MetricsMiddleware
    {
        private readonly IMetrics _metrics;
        private readonly RequestDelegate _next;

        public MetricsMiddleware(RequestDelegate next, IMetrics metrics)
        {
            _next = next;
            _metrics = metrics;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var start = _metrics.Clock.Nanoseconds;
                await _next(context);
                var elapsed = _metrics.Clock.Nanoseconds - start;

                RecordRequestTime(context, elapsed);
            }
            catch (Exception)
            {
                RecordUnhandledException(context);

                throw;
            }
        }

        private void RecordUnhandledException(HttpContext context)
        {
            if (context.HasPrzMetricsCurrentRouteName())
            {
                var routeName = context.GetPrzMetricsCurrentRouteName();

                var tags = new MetricTags(new[] { MethodTag, RouteTag },
                                          new[] { context.Request.Method, routeName });
                _metrics.Measure.Counter.Increment(HttpRequestMetricsRegistry.UnhandledExceptionCount, tags);
            }
            else
            {
                _metrics.Measure.Counter.Increment(HttpRequestMetricsRegistry.UnhandledExceptionCount);
            }
        }

        private void RecordRequestTime(HttpContext context, long elapsed)
        {
            if (context.HasPrzMetricsCurrentRouteName())
            {
                var routeName = context.GetPrzMetricsCurrentRouteName();

                var tags = new MetricTags(new[] { HttpStatusCodeTag, MethodTag, RouteTag },
                                          new[] { context.Response.StatusCode.ToString(), context.Request.Method, routeName });
                _metrics.Provider.Timer.Instance(HttpRequestMetricsRegistry.RequestTimer, tags).Record(elapsed, TimeUnit.Nanoseconds);
            }
        }
    }
}
