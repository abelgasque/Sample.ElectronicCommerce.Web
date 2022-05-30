using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DataBase;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Entities.EF;
using Sample.ElectronicCommerce.Shared.Entities.Mapping;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using Sample.ElectronicCommerce.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Sample.ElectronicCommerce.Shared.Repositories
{
    public class LogAppRepository
    {
        #region Variables
        private readonly ILogger<LogAppRepository> _logger;

        private readonly AppSettings _appSettings;

        private readonly SharedDbContext _context;

        private readonly DataBaseHelper _dbHelper;
        #endregion

        #region Constructor
        public LogAppRepository(
            ILogger<LogAppRepository> logger, 
            IOptions<AppSettings> appSettings, 
            SharedDbContext context, 
            DataBaseHelper dbHelper
        ) {
            _logger = logger;
            _appSettings = appSettings.Value;
            _context = context;
            _dbHelper = dbHelper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.LogApp.Add(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.InsertAsync => SqlException: {ex.Message}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("LogAppRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.LogApp.Update(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.UpdateAsync => SqlException: {ex.Message}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.UpdateAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("LogAppRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(long pId)
        {
            _logger.LogInformation("LogAppRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<LogAppEntity> query = _context.LogApp.AsNoTracking().Where(e => e.Id == pId);
                LogAppEntity entity = await query.FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetById => SqlException: {ex.Message}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation("LogAppRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation("LogAppRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<LogAppEntity> query = _context.LogApp.AsNoTracking().Take(10000);
                query = (pIsActive != null) ? query.Where(e => e.IsActive == pIsActive.Value) : query;
                List<LogAppEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetAll => SqlException: {ex.Message}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("LogAppRepository.GetAll > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetLogAppDay()
        {
            _logger.LogInformation("LogAppRepository.GetLogAppDay => Start");
            ResponseDTO responseDTO;
            try
            {
                DateTime dtStartRange = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); ;
                DateTime dtEndRange = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                IQueryable<LogAppEntity> query = _context.LogApp.AsNoTracking().Take(10000);
                query = query.Where(e => e.DtCreation >= dtStartRange && e.DtCreation <= dtEndRange && e.IsActive == true);
                List<LogAppEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetLogAppDay => SqlException: {ex.Message}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetLogAppDay => Exception: {ex.Message}");
            }

            _logger.LogInformation("LogAppRepository.GetLogAppDay => Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetLogAppForChartDynamic(bool pMustFilterYear)
        {
            _logger.LogInformation("LogAppRepository.GetLogAppForChartDynamic => Start");
            ResponseDTO responseDTO;
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbHelper.SharedConnectionStringSql()))
                {
                    _logger.LogInformation("LogAppRepository.GetLogAppForChartDynamic => Running procedure: " + DataBaseConstant.SPR_WS_GET_LOG_APP_FOR_CHART_DYNAMIC);
                    await connection.OpenAsync();
                    GridReader reader = await connection.QueryMultipleAsync(_dbHelper.GetLogAppForChartDynamic(pMustFilterYear));
                    var listEntities = reader.Read().AsList();
                    bool isSuccess = (listEntities != null && listEntities.Count() > 0) ? true : false;
                    string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                    object dataObject = null;
                    if (isSuccess)
                    {
                        dataObject = (pMustFilterYear)
                                        ? listEntities.Select(row => new GetLogAppForChartYearDb(row)).ToList()
                                          : listEntities.Select(row => new GetLogAppForChartMonthDb(row)).ToList();
                    }
                    responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
                }
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetLogAppForChartDynamic => SqlException: {ex.Message}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppRepository.GetLogAppForChartDynamic => Exception: {ex.Message}");
            }

            _logger.LogInformation("LogAppRepository.GetLogAppForChartDynamic => Finish");
            return responseDTO;
        }
        #endregion
    }
}
