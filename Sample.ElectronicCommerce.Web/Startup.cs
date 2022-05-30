using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.ElectronicCommerce.BrokerChat.Hubs;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using Sample.ElectronicCommerce.Web.Configurations;

namespace Sample.ElectronicCommerce.Web
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.MainConfiguration(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appSettingsSection = _configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("Default");
            //app.UseExceptionMiddleware();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<BrokerChatHub>("/broker/chat/all");
            });         

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"v{appSettings.Version}/swagger.json", $"ElectronicCommerce v{appSettings.Version}"));

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientSide";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
