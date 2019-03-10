using App.Metrics;
using App.Metrics.AspNetCore.Endpoints;
using App.Metrics.Formatters.InfluxDB;
using Microsoft.Extensions.DependencyInjection;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPrzMetrics(this IServiceCollection services, string serviceName, int? port = null)
        {
            if (port != null)
            {
                services.Configure<MetricsEndpointsHostingOptions>(o => o.MetricsEndpointPort = port);
            }

            var metricsOptions = new MetricsOptions
            {
                Enabled = true,
                GlobalTags = new GlobalMetricTags { { "service_name", serviceName } }
            };

            var metricsRoot = new MetricsBuilder().Configuration.Configure(metricsOptions)
                                                  .Build();

            services.AddMetrics(metricsRoot);
            services.AddMetricsEndpoints(o => o.MetricsEndpointOutputFormatter = new MetricsInfluxDbLineProtocolOutputFormatter(new MetricFields()));
            services.AddMetricsTrackingMiddleware(o => o.ApdexTrackingEnabled = false);

            return services;
        }
    }
}