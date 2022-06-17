using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class JsonWebTokenService
    {
        #region Variables
        private readonly ILogger<JsonWebTokenService> _logger;

        private readonly SecuritySettings _securitySettings;

        private readonly AppSettings _appSettings;

        private readonly UserService _userService;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public JsonWebTokenService(
            ILogger<JsonWebTokenService> logger,
            IOptions<SecuritySettings> securitySettings,
            IOptions<AppSettings> appSettings,
            UserService userService,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _securitySettings = securitySettings.Value;
            _appSettings = appSettings.Value;
            _userService = userService;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods    
        private TokenDTO GenerateToken(UserEntity pEntity)
        {
            _logger.LogInformation("JsonWebTokenService.GenerateToken => Start");
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

                List<Claim> payload = new List<Claim>()
                {
                    new Claim("id", pEntity.Id),
                    new Claim("name", pEntity.Name),
                    new Claim("lastName", pEntity.LastName),
                    new Claim("imageUrl", pEntity.ImageUrl),
                    new Claim("mail", pEntity.Mail),
                    new Claim("nuCellPhone", pEntity.NuCellPhone),
                };

                if (pEntity.Roles != null && pEntity.Roles.Count > 0)
                {
                    foreach (string role in pEntity.Roles)
                    {
                        Claim claim = new Claim(ClaimTypes.Role, role);
                        payload.Add(claim);
                    }
                }
                
                tokenDescriptor.Subject = new ClaimsIdentity(payload);
                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenWs = new TokenDTO()
                {
                    access_token = tokenHandler.WriteToken(token),
                    expires_in = _securitySettings.ExpireIn,
                    token_type = _securitySettings.Type
                };
                _logger.LogInformation($"JsonWebTokenService.GenerateToken => AccessToken: Generated, AccessToken Expire: {DateTime.UtcNow.AddMinutes(_securitySettings.ExpireIn).ToString("dd/MM/yyyy - HH:mm:ss")}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"JsonWebTokenService.GenerateToken => Exception: {ex.Message}");
                tokenWs = null;
            }
            _logger.LogInformation("JsonWebTokenService.GenerateToken => End");
            return tokenWs;
        }

        public async Task<ReturnDTO> Login(UserDTO pEntity)
        {
            _logger.LogInformation($"JsonWebTokenService.Login => Start");
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
                    int countBlock = 5;
                    bool isFail = false;
                    if (user.IsBlock)
                    {
                        isFail = true;
                        responseDTO = new ResponseDTO(false, "Usuário bloqueado!", null);
                    }
                    else if ((!user.IsActive))
                    {
                        isFail = true;
                        user.NuAuthAttemptsFail += 1;
                        responseDTO = new ResponseDTO(false, "Usuário inátivo!", null);
                    }
                    else if (!user.Password.Equals(pEntity.Password))
                    {
                        isFail = true;
                        user.NuAuthAttemptsFail += 1;
                        responseDTO = new ResponseDTO(false, $"Senha incorreta, restam mais {(countBlock - user.NuAuthAttemptsFail)} tentativas!", null);
                    }
                    else if (((user.NuAuthAttemptsFail + 1) == countBlock) && (isFail))
                    {
                        user.IsBlock = true;
                        user.NuAuthAttemptsFail = 0;
                        responseDTO = new ResponseDTO(false, "Usuário bloqueado, e-mail enviado para caixa de entrada!", null);
                    }
                    else
                    {
                        user.NuAuthAttemptsFail = 0;
                        responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, this.GenerateToken(user));
                    }
                    await _userService.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"JsonWebTokenService.Login => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserSession.Login", pEntity, responseDTO);
            _logger.LogInformation($"JsonWebTokenService.Login => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> Refresh(string pId)
        {
            _logger.LogInformation($"JsonWebTokenService.Refresh => Start");
            ResponseDTO responseDTO;
            try
            {
                ReturnDTO returnDTO = await _userService.GetById(pId);
                if (returnDTO.IsSuccess)
                {
                    UserEntity entity = (UserEntity)returnDTO.ResultObject;
                    returnDTO.ResultObject = this.GenerateToken(entity);
                }
                responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"JsonWebTokenService.Refresh => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserSession.Refresh", null, responseDTO);
            _logger.LogInformation($"JsonWebTokenService.Refresh => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
