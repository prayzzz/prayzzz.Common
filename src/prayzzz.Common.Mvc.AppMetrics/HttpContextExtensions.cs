using Microsoft.AspNetCore.Http;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public static class HttpContextExtensions
    {
        public static bool HasPrzMetricsCurrentRouteName(this HttpContext context)
        {
            return context.Items.ContainsKey(PrzMetricConstants.MetricsCurrentRouteName);
        }

        public static void AddPrzMetricsCurrentRouteName(this HttpContext context, string route)
        {
            if (context.Items.ContainsKey(PrzMetricConstants.MetricsCurrentRouteName))
            {
                return;
            }

            context.Items.Add(PrzMetricConstants.MetricsCurrentRouteName, route.ToLowerInvariant());
        }

        public static string GetPrzMetricsCurrentRouteName(this HttpContext context)
        {
            if (context.Items.TryGetValue(PrzMetricConstants.MetricsCurrentRouteName, out var obj))
            {
                if (obj is string str)
                {
                    return str;
                }
            }

            return string.Empty;
        }
    }
}
