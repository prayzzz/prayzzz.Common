using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public class MvcMetricsFilter : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var routeName = context.ActionDescriptor?.AttributeRouteInfo?.Template.ToLowerInvariant();
            if (routeName != null)
            {
                context.HttpContext.AddPrzMetricsCurrentRouteName(routeName);
            }

            await next.Invoke();
        }
    }
}
