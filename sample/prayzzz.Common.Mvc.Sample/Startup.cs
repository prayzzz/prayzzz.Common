using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using prayzzz.Common.Mvc.AppMetrics;

namespace prayzzz.Common.Mvc.Sample
{
    public class Startup
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPrzMetrics("SampleApp");

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_0)
                    .AddPrzMetrics();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UsePrzMetrics();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default_area", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
