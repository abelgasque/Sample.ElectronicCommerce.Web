using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Entities.EF;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Helpers;
using Serilog;
using Sample.ElectronicCommerce.BrokerMail.Repositories;
using Sample.ElectronicCommerce.BrokerMail.Services;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.BrokerChat.Repositories;
using Sample.ElectronicCommerce.BrokerChat.Services;

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
                options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
            services.Configure<MongoClientSettings>(configuration.GetSection("MongoClientSettings"));
            services.Configure<SecuritySettings>(configuration.GetSection("SecuritySettings"));
            services.Configure<SharedSettings>(configuration.GetSection("SharedSettings"));

            //BrokerMail
            services.AddTransient<MailRepository>();
            services.AddTransient<MailBrokerRepository>();
            services.AddTransient<MailMessageRepository>();
            services.AddTransient<MailService>();
            services.AddTransient<MailBrokerService>();
            services.AddTransient<MailMessageService>();

            //Core
            services.AddDbContext<SharedDbContext>();
            services.AddTransient<MailHelper>();
            services.AddTransient<ApplicationRepository>();
            services.AddTransient<ApplicationService>();            
            services.AddTransient<LogAppRepository>();
            services.AddTransient<LogAppService>();
            services.AddTransient<UserSessionRepository>();
            services.AddTransient<UserSessionService>();

            //Security
            services.AddTransient<UserRepository>();
            services.AddTransient<UserRoleRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<UserRoleService>();

            //WebSocket
            services.AddTransient<ChatBrokerRepository>();
            services.AddTransient<ChatBrokerService>();

            return services;
        }
    }
}
