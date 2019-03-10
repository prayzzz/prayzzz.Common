using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Timer;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public static class HttpRequestMetricsRegistry
    {
        public static readonly CounterOptions UnhandledExceptionCount = new CounterOptions
        {
            Name = "Exceptions",
            MeasurementUnit = Unit.Errors
        };

        public static readonly TimerOptions RequestTimer = new TimerOptions
        {
            Name = "Requests",
            MeasurementUnit = Unit.Requests
        };
    }
}