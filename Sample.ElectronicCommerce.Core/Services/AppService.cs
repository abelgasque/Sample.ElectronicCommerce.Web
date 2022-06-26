using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class AppService
    {
        #region Variables
        private readonly ILogger<AppService> _logger;

        private readonly AppSettings _appSettings;

        private readonly EnvironmentSettings _environmentSettings;
        #endregion

        #region Constructor
        public AppService(
            ILogger<AppService> logger,
            IOptions<AppSettings> appSettings,
            IOptions<EnvironmentSettings> environmentSettings
        )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _environmentSettings = environmentSettings.Value;
        }
        #endregion

        #region Methods
        public ReturnDTO GetAppSettingsByKey(string pKey)
        {
            _logger.LogInformation($"AppService.GetByKey => Start");
            if (!_environmentSettings.Key.Equals(pKey))
            {
                throw new UnauthorizedException("Acesso não autorizado!");
            }
            return new ReturnDTO(true, "OK", _appSettings);
        }
        #endregion
    }
}