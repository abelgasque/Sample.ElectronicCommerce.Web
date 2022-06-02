using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserSessionService
    {
        #region Variables
        private readonly ILogger<UserSessionService> _logger;
        
        private readonly SecuritySettings _securitySettings;

        private readonly AppSettings _appSettings;

        private readonly UserSessionRepository _repository;        

        private readonly UserService _userService;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public UserSessionService(
            ILogger<UserSessionService> logger, 
            IOptions<SecuritySettings> securitySettings, 
            IOptions<AppSettings> appSettings, 
            UserSessionRepository repository, 
            UserService userService, 
            LogAppService logAppService
        ) {
            _logger = logger;
            _securitySettings = securitySettings.Value;
            _appSettings = appSettings.Value;
            _repository = repository;
            _userService = userService;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods        
        public async Task<ReturnDTO> InsertAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation($"UserSessionService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.InsertAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserSessionService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserSessionService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation($"UserSessionService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtLastUpdate = DateTime.Now;
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.UpdateAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserSessionService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserSessionService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"UserSessionService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
                if (responseDTO.IsSuccess)
                {
                    UserSessionEntity entity = (UserSessionEntity)responseDTO.DataObject;
                    ReturnDTO returnDTO = await _userService.GetById(entity.IdUser.ToString());
                    if (returnDTO.IsSuccess && responseDTO.DataObject != null)
                    {
                        UserEntity user = (UserEntity)returnDTO.ResultObject;
                        entity.User = user;
                    }
                }                
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserSessionService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation($"UserSessionService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserSessionService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion

        #region Methods Token    
        private string GenerateToken(ICollection<RoleEntity> pRoles)
        {
            _logger.LogInformation("UserSessionService.GenerateToken => Start");
            TokenDTO tokenWs;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_securitySettings.Secret);
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddMinutes(_securitySettings.ExpireIn),
                    NotBefore = DateTime.Now,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                if (pRoles != null && pRoles.Count > 0)
                {
                    List<Claim> roles = new List<Claim>();
                    foreach (RoleEntity role in pRoles)
                    {
                        Claim claim = new Claim(ClaimTypes.Role, role.Code);
                        roles.Add(claim);
                    }
                    tokenDescriptor.Subject = new ClaimsIdentity(roles);
                }
                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenWs = new TokenDTO()
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    ExpiresIn = _securitySettings.ExpireIn
                };
                _logger.LogInformation($"UserSessionService.GenerateToken => AccessToken: Generated, AccessToken Expire: {DateTime.UtcNow.AddMinutes(_securitySettings.ExpireIn).ToString("dd/MM/yyyy - HH:mm:ss")}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserSessionService.GenerateToken => Exception: {ex.Message}");
                tokenWs = null;
            }
            _logger.LogInformation("UserSessionService.GenerateToken => End");
            return tokenWs.AccessToken;
        }

        public async Task<ReturnDTO> Login(UserDTO pEntity)
        {
            _logger.LogInformation($"UserSessionService.Login => Start");
            ResponseDTO responseDTO;
            try
            {
                ReturnDTO returnDTO = await _userService.GetByMail(pEntity.Mail);
                UserEntity user = (UserEntity)returnDTO.ResultObject;
                if (user == null)
                {
                    responseDTO = new ResponseDTO(false, AppConstant.DeMessageDataNotFoundWS, null);
                }
                else 
                {                    
                    responseDTO = await _repository.GetByIdUser(user.Id);
                    UserSessionEntity entity;                    
                    if ((!user.IsActive) || (user.IsBlock))
                    {
                        responseDTO = new ResponseDTO(false, "Usuário inátivo ou bloqueado!", null);
                    }
                    else if ((!responseDTO.IsSuccess) || (responseDTO.DataObject == null))
                    {
                        entity = new UserSessionEntity();
                        entity.IdUser = user.Id;
                        entity.User = user;
                        entity.Version = _appSettings.Version;
                        entity.IsTest = _appSettings.IsTest;
                        entity.Password = "1";
                        entity.AccessToken = this.GenerateToken(new List<RoleEntity>());
                        responseDTO = await _repository.InsertAsync(entity);
                    }
                    else
                    {
                        entity = (UserSessionEntity)responseDTO.DataObject;
                        bool isSuccess = false;
                        string deMessage = AppConstant.DeMessageSuccessWS;
                        if (!entity.Password.Equals(pEntity.Password))
                        {                            
                            entity.NuFailsToken += 1;
                            deMessage = "Senha incorreta!";
                        }
                        else
                        {
                            isSuccess = true;
                            entity.NuSuccessToken += 1;
                            entity.AccessToken = this.GenerateToken(entity.Roles);                            
                            
                        }
                        entity.NuAuthAttemptsToken += 1;
                        returnDTO = await this.UpdateAsync(entity);
                        responseDTO = new ResponseDTO(isSuccess, deMessage, returnDTO.ResultObject);
                    }
                }                
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.Login => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserSession.Login", pEntity, responseDTO);
            _logger.LogInformation($"UserSessionService.Login => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> Refresh(TokenDTO pEntity)
        {
            _logger.LogInformation($"UserSessionService.Refresh => Start");
            ResponseDTO responseDTO;
            try
            {
                ReturnDTO returnDTO = await this.GetById(pEntity.IdUserSession);
                if (returnDTO.IsSuccess)
                {
                    UserSessionEntity entity = (UserSessionEntity)returnDTO.ResultObject;
                    entity.NuSuccessToken += 1;
                    entity.AccessToken = this.GenerateToken(entity.Roles);
                    await this.UpdateAsync(entity);
                    returnDTO = await this.GetById(pEntity.IdUserSession);
                }
                responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.Refresh => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserSession.Refresh", null, responseDTO);
            _logger.LogInformation($"UserSessionService.Refresh => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
