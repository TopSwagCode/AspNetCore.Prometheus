using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace WebApi
{
    public class Startup
    {

        /*
            TODO:
            * Add Different Exception types and see them being caught and properly handled by prometheus
            * Create Youtube clip showing the project and how to get "good" dashboards.
            * Show "fallgroups" / gotchas
        */

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseMetricServer(); //Base metrics only. Import to be after Routing and before Endpoints
            app.UseCustomRequestMiddleware(); 
            app.UseHttpMetrics();

            if (env.IsDevelopment()) // https://github.com/prometheus-net/prometheus-net/issues/150
            {
                app.UseDeveloperExceptionPage();
            }
            else{
                app.UseExceptionHandler("/error");
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
