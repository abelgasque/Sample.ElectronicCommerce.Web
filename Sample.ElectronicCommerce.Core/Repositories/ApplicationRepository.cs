using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities;
using Sample.ElectronicCommerce.Core.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Repositories
{
    public class ApplicationRepository
    {
        #region Variables
        private readonly ILogger<ApplicationRepository> _logger;

        private readonly CoreDbContext _context;

        private readonly AppSettings _appSettings;
        #endregion

        #region Constructor
        public ApplicationRepository(ILogger<ApplicationRepository> logger, CoreDbContext context, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _context = context;
            _appSettings = appSettings.Value;
        }
        #endregion

        #region Methods        
        public async Task<ResponseDTO> GetByApplication()
        {
            _logger.LogInformation("ApplicationRepository.GetByApplication => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<ApplicationEntity> query = _context.Application.AsNoTracking();
                //query = query.Where(e => e.Id == _appSettings.IdApplication).Where(e => e.IsActive == true);
                ApplicationEntity entity = await query.FirstOrDefaultAsync();
                bool isSuccess = (entity != null);
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"ApplicationRepository.GetByApplication => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"ApplicationRepository.GetByApplication => Exception: { ex.Message }");
            }
            _logger.LogInformation("ApplicationRepository.GetByApplication > Finish");
            return responseDTO;
        }
        #endregion
    }
}
