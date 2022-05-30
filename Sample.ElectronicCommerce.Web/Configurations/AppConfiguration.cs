using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF;
using Sample.ElectronicCommerce.BrokerMail.Repositories;
using Sample.ElectronicCommerce.BrokerMail.Services;
using Sample.ElectronicCommerce.Core.Entities;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Security.Configurations;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Shared.Entities.EF;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using Sample.ElectronicCommerce.Shared.Helpers;
using Sample.ElectronicCommerce.Shared.Repositories;
using Sample.ElectronicCommerce.Shared.Services;
using Sample.ElectronicCommerce.WebSocket.Repositories;
using Sample.ElectronicCommerce.WebSocket.Services;
using Serilog;

namespace Sample.ElectronicCommerce.Web.Configurations
{
    public static class AppConfiguration
    {
        public static IServiceCollection MainConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var tokenSettingsSection = configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(tokenSettingsSection);

            var brokerMailSettingsSection = configuration.GetSection("BrokerMailSettings");
            services.Configure<BrokerMailSettings>(brokerMailSettingsSection);

            var coreSettingsSection = configuration.GetSection("CoreSettings");
            services.Configure<CoreSettings>(coreSettingsSection);

            var securitySettingsSection = configuration.GetSection("SecuritySettings");
            services.Configure<SecuritySettings>(securitySettingsSection);

            var sharedSettingsSection = configuration.GetSection("SharedSettings");
            services.Configure<SharedSettings>(sharedSettingsSection);

            var webSocketSettingsSection = configuration.GetSection("WebSocketSettings");
            services.Configure<WebSocketSettings>(webSocketSettingsSection);

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Default", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientSide/dist";
            });

            services.AddLogging(configure => configure.AddSerilog());
            services.AddControllers();

            services.SetAuthConfiguration(configuration);
            services.SetSwaggerConfiguration(configuration);  

            //BrokerMail
            services.AddDbContext<MailBrokerDbContext>();
            services.AddTransient<MailRepository>();
            services.AddTransient<MailBrokerRepository>();
            services.AddTransient<MailMessageRepository>();
            services.AddTransient<MailService>();
            services.AddTransient<MailBrokerService>();
            services.AddTransient<MailMessageService>();

            //Core
            services.AddDbContext<CoreDbContext>();
            services.AddTransient<ApplicationRepository>();
            services.AddTransient<ApplicationService>();

            //Security
            services.AddDbContext<SecurityDbContext>();
            services.AddTransient<UserRepository>();
            services.AddTransient<UserRoleRepository>();
            services.AddTransient<UserHasRoleRepository>();
            services.AddTransient<UserSessionRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<UserRoleService>();
            services.AddTransient<UserHasRoleService>();
            services.AddTransient<UserSessionService>();

            //Shared
            services.AddDbContext<SharedDbContext>();
            services.AddTransient<DataBaseHelper>();
            services.AddTransient<MailHelper>();
            services.AddTransient<LogAppRepository>();
            services.AddTransient<LogAppService>();

            //WebSocket
            services.AddTransient<ChatRepository>();
            services.AddTransient<ChatService>();

            return services;
        }
    }
}
