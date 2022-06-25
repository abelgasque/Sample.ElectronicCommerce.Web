using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
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

        private readonly EnvironmentSettings _environmentSettings;

        private readonly AppSettings _appSettings;

        private readonly UserService _userService;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public JsonWebTokenService(
            ILogger<JsonWebTokenService> logger,
            IOptions<EnvironmentSettings> environmentSettings,
            IOptions<AppSettings> appSettings,
            UserService userService,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _environmentSettings = environmentSettings.Value;
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
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_environmentSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_environmentSettings.ExpireIn),
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
                expires_in = _environmentSettings.ExpireIn,
                token_type = _environmentSettings.Type
            };
            _logger.LogInformation(
                "JsonWebTokenService.GenerateToken => AccessToken: Generated, "
                + $"AccessToken Expire: {DateTime.UtcNow.AddMinutes(_environmentSettings.ExpireIn).ToString("dd/MM/yyyy - HH:mm:ss")}"
            );
            _logger.LogInformation("JsonWebTokenService.GenerateToken => End");
            return tokenWs;
        }

        public async Task<ReturnDTO> Login(UserDTO pEntity)
        {
            _logger.LogInformation($"JsonWebTokenService.Login => Start");
            ResponseDTO responseDTO;
            ReturnDTO returnDTO = await _userService.GetByMail(pEntity.Mail);
            UserEntity user = (UserEntity)returnDTO.ResultObject;
            if (user == null)
            {
                throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            }

            user.NuAuthAttemptsFail += 1;
            await _userService.UpdateAsync(user);
            if ((user.NuAuthAttemptsFail >= _environmentSettings.NuAuthAttempts)
                && (!user.Password.Equals(pEntity.Password)))
            {
                user.IsBlock = true;
                user.NuAuthAttemptsFail = 0;
                responseDTO = new ResponseDTO(false, "Usuário bloqueado, e-mail enviado para caixa de entrada!", null);
            }

            if (user.IsBlock)
            {
                throw new BadRequestException("Usuário bloqueado!");
            }

            if (!user.IsActive)
            {
                throw new BadRequestException("Usuário inátivo!");
            }

            if (!user.Password.Equals(pEntity.Password))
            {
                throw new BadRequestException($"Senha incorreta, restam mais {(_environmentSettings.NuAuthAttempts - user.NuAuthAttemptsFail)} tentativas!");
            }

            responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, this.GenerateToken(user));
            await _logAppService.AppInsertAsync(null, "UserSession.Login", pEntity, responseDTO);
            _logger.LogInformation($"JsonWebTokenService.Login => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> Refresh(string pId)
        {
            _logger.LogInformation($"JsonWebTokenService.Refresh => Start");
            ReturnDTO returnDTO = await _userService.GetById(pId);
            if (returnDTO.IsSuccess)
            {
                UserEntity entity = (UserEntity)returnDTO.ResultObject;
                returnDTO.ResultObject = this.GenerateToken(entity);
            }
            ResponseDTO responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
            await _logAppService.AppInsertAsync(null, "UserSession.Refresh", null, responseDTO);
            _logger.LogInformation($"JsonWebTokenService.Refresh => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}