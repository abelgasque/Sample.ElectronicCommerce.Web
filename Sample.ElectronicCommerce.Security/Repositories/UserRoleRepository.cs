using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserRoleRepository
    {
        #region Variables
        private readonly ILogger<UserRoleRepository> _logger;

        private readonly SecurityDbContext _context;
        #endregion

        #region Constructor
        public UserRoleRepository(ILogger<UserRoleRepository> logger, SecurityDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        #endregion

        #region Methods        
        public async Task<ResponseDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation("UserRoleRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserRoleEntity> query = _context.UserRole.AsNoTracking().Take(10000);
                query = (pIsActive != null) ? query.Where(e => e.IsActive == pIsActive.Value) : query;
                List<UserRoleEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleRepository.GetAll => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRoleRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
