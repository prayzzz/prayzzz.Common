using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace prayzzz.Common.Mvc.AppMetrics
{
    public class MvcMetricsFilter : IAsyncResourceFilter
    {
        private const string AreaRouteKey = "area";
        private const string ControllerRouteKey = "controller";
        private const string ActionRouteKey = "action";
        
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var routeName = context.ActionDescriptor?.AttributeRouteInfo?.Template;
            if (routeName != null)
            {
                context.HttpContext.AddPrzMetricsCurrentRouteName(routeName);
            }
            else if (context.RouteData.Values.Any())
            {
                var area = context.RouteData.Values.FirstOrDefault(v => v.Key == AreaRouteKey);
                var controller = context.RouteData.Values.FirstOrDefault(v => v.Key == ControllerRouteKey);
                var action = context.RouteData.Values.FirstOrDefault(v => v.Key == ActionRouteKey);

                var route = (area.Value == null ? string.Empty : $"{area.Value}/") + $"{controller.Value}/{action.Value}";
                context.HttpContext.AddPrzMetricsCurrentRouteName(route);
            }

            await next.Invoke();
        }
    }
}
