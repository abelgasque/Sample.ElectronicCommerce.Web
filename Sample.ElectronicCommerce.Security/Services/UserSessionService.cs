using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using Sample.ElectronicCommerce.Shared.Services;
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
        
        private readonly TokenSettings _tokenSettings;

        private readonly AppSettings _appSettings;

        private readonly UserSessionRepository _repository;        

        private readonly UserService _userService;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public UserSessionService(
            ILogger<UserSessionService> logger, 
            IOptions<TokenSettings> tokenSettings, 
            IOptions<AppSettings> appSettings, 
            UserSessionRepository repository, 
            UserService userService, 
            LogAppService logAppService
        ) {
            _logger = logger;
            _tokenSettings = tokenSettings.Value;
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
            await _logAppService.AppInsertAsync(pEntity.Id, "UserSessionService.InsertAsync", pEntity, responseDTO);
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
            await _logAppService.AppInsertAsync(pEntity.Id, "UserSessionService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserSessionService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(long pId)
        {
            _logger.LogInformation($"UserSessionService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
                if (responseDTO.IsSuccess)
                {
                    UserSessionEntity entity = (UserSessionEntity)responseDTO.DataObject;
                    ReturnDTO returnDTO = await _userService.GetById(entity.IdUser);
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

        public async Task<ReturnDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation($"UserSessionService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll(pIsActive);
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
        private List<string> ValidateUserSession(UserDTO pEntity, UserEntity pUser, bool pIsUserSystem)
        {
            List<string> listValidate = new List<string>();

            if (pIsUserSystem)
            {
                int count = 0;
                foreach (UserRoleEntity role in pUser.Roles)
                {
                    if (role.Code.Equals(UserRoleConstant.CodeSystem))
                    {
                        count++;
                    }
                }

                if (count <= 0)
                {
                    listValidate.Add("Usuário não possui permissão de sistema!");
                }
            }

            if (!pUser.IsActive)
            {
                listValidate.Add("Usuário inativo!");
            }

            if (pUser.IsBlock)
            {
                listValidate.Add("Usuário bloqueado!");
            }

            if (!pUser.Password.Equals(pEntity.Password))
            {
                listValidate.Add("Senha incorreta!");
            }

            return listValidate;
        }        

        private TokenDTO GenerateToken(List<UserRoleEntity> pRoles)
        {
            _logger.LogInformation("UserSessionService.GenerateToken => Start");
            TokenDTO tokenWs;
            try
            {                
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpireIn),
                    NotBefore = DateTime.Now,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                if (pRoles != null && pRoles.Count > 0)
                {
                    List<Claim> roles = new List<Claim>();
                    foreach (UserRoleEntity role in pRoles)
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
                    ExpiresIn = _tokenSettings.ExpireIn
                };
                _logger.LogInformation($"UserSessionService.GenerateToken => AccessToken: Generated, AccessToken Expire: { tokenWs.ExpiresIn.ToString("dd/MM/yyyy - HH:mm:ss") }");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserSessionService.GenerateToken => Exception: { ex.Message }");
                tokenWs = null;
            }
            _logger.LogInformation("UserSessionService.GenerateToken => End");
            return tokenWs;
        }
        
        public async Task<ReturnDTO> Login(UserDTO pEntity, bool pIsUserSystem)
        {
            _logger.LogInformation($"UserSessionService.Login => Start");
            long idUserSession = 0;
            ResponseDTO responseDTO;
            try
            {
                ReturnDTO returnDTO = await _userService.GetByMail(pEntity.Mail);
                UserEntity user = (UserEntity)returnDTO.ResultObject;
                returnDTO = new ReturnDTO(false, AppConstant.DeMessageDataNotFoundWS, null);

                if (user == null)
                {
                    responseDTO = new ResponseDTO(false, AppConstant.DeMessageDataNotFoundWS, null);
                }
                else
                {
                    responseDTO = await _repository.GetByIdUserWithAuthFailed(user.Id);
                    List<string> listValidate = this.ValidateUserSession(pEntity, user, pIsUserSystem);
                    bool isSuccess = (listValidate.Count <= 0);
                    string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageInvalidModel;
                    object dataObject = (isSuccess) ? null : listValidate;
                    returnDTO = new ReturnDTO(isSuccess, deMessage, dataObject);
                    if (responseDTO.IsSuccess)
                    {
                        UserSessionEntity entity = (UserSessionEntity)responseDTO.DataObject;                        
                        if (!isSuccess && (entity.NuAuthenticationAttempts + 1) > _appSettings.NuAuthenticationAttempts)
                        {
                            listValidate.Add("Usuário bloqueado!");
                            //Criar método para bloquear usuário e enviar e-mail de recuperação                            
                        }
                        else
                        {                            
                            entity.IsSuccess = isSuccess;
                            entity.DeMessage = deMessage;
                            entity.NuAuthenticationAttempts += 1;
                            if (isSuccess)
                            {
                                TokenDTO tokenWs = this.GenerateToken(user.Roles);
                                entity.AccessToken = tokenWs.AccessToken;
                                entity.DtAccessTokenExpiration = DateTime.Now.AddMinutes(tokenWs.ExpiresIn);
                            }
                            returnDTO = await this.UpdateAsync(entity);
                            if (returnDTO.IsSuccess) { idUserSession = entity.Id; }
                        }
                    }
                    else
                    {
                        TokenDTO tokenWs = this.GenerateToken(user.Roles);
                        UserSessionEntity persistSession = new UserSessionEntity()
                        {
                            Id = 0,
                            IdUser = user.Id,
                            DtCreation = DateTime.Now,
                            DtLastUpdate = null,
                            DtAccessTokenExpiration = (isSuccess) ? DateTime.Now.AddMinutes(tokenWs.ExpiresIn) : null,
                            DtRefreshTokenExpiration = null,
                            AccessToken = (isSuccess) ? tokenWs.AccessToken : null,
                            RefreshToken = null,
                            DeMessage = deMessage,
                            NuVersion = _appSettings.Version,
                            NuAuthenticationAttempts = 1,
                            NuRefreshToken = null,
                            IsSuccess = isSuccess,
                            IsTest = _appSettings.IsTest,
                            IsActive = true,
                        };
                        returnDTO = await this.InsertAsync(persistSession);
                        if (returnDTO.IsSuccess) { idUserSession = persistSession.Id; }
                    }
                }
                if (idUserSession > 0) { returnDTO = await this.GetById(idUserSession); }
                responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.Login => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(idUserSession, "UserSession.Login", pEntity, responseDTO);
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
                    if (DateTime.Now >= entity.DtAccessTokenExpiration)
                    {
                        TokenDTO tokenWs = this.GenerateToken(entity.User.Roles);
                        entity.RefreshToken = tokenWs.AccessToken;
                        entity.DtRefreshTokenExpiration = DateTime.Now.AddMinutes(tokenWs.ExpiresIn);
                        await this.UpdateAsync(entity);
                        returnDTO = await this.GetById(pEntity.IdUserSession);
                    }
                    else
                    {
                        returnDTO = new ReturnDTO(true, "Token de acesso ainda está ativo!", entity);
                    }
                }
                responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.Refresh => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(pEntity.IdUserSession, "UserSession.Refresh", null, responseDTO);
            _logger.LogInformation($"UserSessionService.Refresh => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
