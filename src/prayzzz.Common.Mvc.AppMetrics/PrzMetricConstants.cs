namespace prayzzz.Common.Mvc.AppMetrics
{
    public class PrzMetricConstants
    {
        public const string RouteTag = "route";
        public const string MethodTag = "method";
        public const string HttpStatusCodeTag = "http_status_code";

        public const string MetricsCurrentRouteName = "__App.Metrics.CurrentRouteName__";

        public const string MetricsEndpointConfigKey = "Kestrel:Endpoints:MetricHttp:Url";
    }
}
