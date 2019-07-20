using Microsoft.AspNetCore.Builder;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UsePrzMetrics(this IApplicationBuilder app)
        {
            app.UseMiddleware<MetricsMiddleware>();
            app.UseMetricsEndpoint();

            return app;
        }
    }
}
