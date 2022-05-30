using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.ElectronicCommerce.BrokerChat.Repositories;
using Sample.ElectronicCommerce.BrokerChat.Services;
using Sample.ElectronicCommerce.BrokerMail.Repositories;
using Sample.ElectronicCommerce.BrokerMail.Services;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Security.Configurations;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Shared.Entities.EF;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using Sample.ElectronicCommerce.Shared.Helpers;
using Sample.ElectronicCommerce.Shared.Repositories;
using Sample.ElectronicCommerce.Shared.Services;
using Serilog;

namespace Sample.ElectronicCommerce.Web.Configurations
{
    public static class AppConfiguration
    {
        public static IServiceCollection MainConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {            
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

            //Configure settings
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<BrokerChatSettings>(configuration.GetSection("BrokerChatSettings"));
            services.Configure<BrokerMailSettings>(configuration.GetSection("BrokerMailSettings"));
            services.Configure<CoreSettings>(configuration.GetSection("CoreSettings"));
            services.Configure<SecuritySettings>(configuration.GetSection("SecuritySettings"));
            services.Configure<SharedSettings>(configuration.GetSection("SharedSettings"));            

            //BrokerMail
            services.AddTransient<MailRepository>();
            services.AddTransient<BrokerMailRepository>();
            services.AddTransient<MailMessageRepository>();
            services.AddTransient<MailService>();
            services.AddTransient<BrokerMailService>();
            services.AddTransient<MailMessageService>();

            //Core
            services.AddTransient<ApplicationRepository>();
            services.AddTransient<ApplicationService>();

            //Security
            services.AddTransient<UserRepository>();
            services.AddTransient<UserRoleRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<UserRoleService>();

            //Shared
            services.AddDbContext<SharedDbContext>();
            services.AddTransient<DataBaseHelper>();
            services.AddTransient<MailHelper>();
            services.AddTransient<LogAppRepository>();
            services.AddTransient<LogAppService>();
            services.AddTransient<UserSessionRepository>();            
            services.AddTransient<UserSessionService>();

            //WebSocket
            services.AddTransient<BrokerChatRepository>();
            services.AddTransient<BrokerChatService>();

            return services;
        }
    }
}
