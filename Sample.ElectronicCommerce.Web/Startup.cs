using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.ElectronicCommerce.Chat.Consumer;
using Sample.ElectronicCommerce.Chat.Repositories;
using Sample.ElectronicCommerce.Chat.Services;
using Sample.ElectronicCommerce.Core.Entities.DataBase.EF;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Extensions;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Mail.Repositories;
using Sample.ElectronicCommerce.Mail.Services;
using Sample.ElectronicCommerce.Security.Extensions;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Security.Services;
using Serilog;

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
            services.AddControllers();
            services.AddLogging(configure => configure.AddSerilog());
            services.AddSignalR(o => { o.EnableDetailedErrors = true; });
            services.Configure<IISOptions>(o => { o.ForwardClientCertificate = false; });
            services.AddCors(o => { o.AddPolicy("AllowOrigin", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
            services.AddSpaStaticFiles(o => { o.RootPath = "ClientSide/dist"; });

            //Extensions
            services.AddSwagger(_configuration);
            services.AddJsonWebToken(_configuration);

            //Configure settings
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            services.Configure<EnvironmentSettings>(_configuration.GetSection("EnvironmentSettings"));
            services.Configure<MongoClientSettings>(_configuration.GetSection("MongoClientSettings"));
            services.Configure<SecuritySettings>(_configuration.GetSection("SecuritySettings"));
            services.Configure<SharedSettings>(_configuration.GetSection("SharedSettings"));

            //Core
            services.AddDbContext<SharedDbContext>();
            services.AddTransient<MailHelper>();
            services.AddTransient<OrganizationRepository>();
            services.AddTransient<OrganizationService>();
            services.AddTransient<LogAppRepository>();
            services.AddTransient<LogAppService>();

            //Security
            services.AddTransient<JsonWebTokenService>();
            services.AddTransient<UserRepository>();
            services.AddTransient<UserRoleRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<UserRoleService>();

            //Chat
            services.AddTransient<ChatBrokerRepository>();
            services.AddTransient<ChatBrokerService>();

            //Mail
            services.AddTransient<MailBrokerRepository>();
            services.AddTransient<MailGroupRepository>();
            services.AddTransient<MailSingleRepository>();
            services.AddTransient<MailBrokerService>();
            services.AddTransient<MailGroupService>();
            services.AddTransient<MailSingleService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appSettingsSection = _configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<ChatConsumer>("/chat");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"v{appSettings.Version}/swagger.json", $"ElectronicCommerce v{appSettings.Version}"));

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientSide";
                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}