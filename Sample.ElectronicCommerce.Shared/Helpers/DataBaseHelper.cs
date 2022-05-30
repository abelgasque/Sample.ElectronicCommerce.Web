using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using System;

namespace Sample.ElectronicCommerce.Shared.Helpers
{
    public class DataBaseHelper
    {
        #region Variables
        private readonly AppSettings _appSettings;

        private readonly CoreSettings _coreSettings;

        private readonly SecuritySettings _securitySettings;

        private readonly SharedSettings _sharedSettings;

        private readonly BrokerMailSettings _brokerMailSettings;
        #endregion

        #region Constructor
        public DataBaseHelper(
            IOptions<AppSettings> appSettings,
            IOptions<BrokerMailSettings> brokerMailSettings,
            IOptions<CoreSettings> coreSettings, 
            IOptions<SecuritySettings> securityDbSettings,
            IOptions<SharedSettings> sharedSettings
        )
        {
            _appSettings = appSettings.Value;
            _brokerMailSettings = brokerMailSettings.Value;
            _coreSettings = coreSettings.Value;
            _securitySettings = securityDbSettings.Value;
            _sharedSettings = sharedSettings.Value;
        }
        #endregion

        #region Methods        
        public string SharedConnectionStringSql()
        {
            return _sharedSettings.DataBase.GetConnectionString;
        }

        public string GetLogAppForChartDynamic(bool pMustFilterYear)
        {
            return $"EXEC {DataBaseConstant.SPR_WS_GET_LOG_APP_FOR_CHART_DYNAMIC} "
                    + $"{DataBaseConstant.P_IS_TEST} = {Convert.ToInt32(_appSettings.IsTest)},"
                    + $"{DataBaseConstant.P_MUST_FILTER_YEAR} = {Convert.ToInt32(pMustFilterYear)};";
        }
        #endregion
    }
}
