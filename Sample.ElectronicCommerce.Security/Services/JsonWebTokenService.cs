using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class JsonWebTokenService
    {
        #region Variables
        private readonly ILogger<JsonWebTokenService> _logger;

        private readonly AppSettings _appSettings;

        private readonly TokenSettings _tokenSettings;

        private readonly UserService _userService;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public JsonWebTokenService(
            ILogger<JsonWebTokenService> logger,
            IOptions<AppSettings> appSettings,
            IOptions<TokenSettings> tokenSettings,
            UserService userService,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _tokenSettings = tokenSettings.Value;
            _userService = userService;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods    
        private TokenDTO GenerateToken(UserEntity pEntity)
        {
            TokenDTO tokenWs;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpireIn),
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
                new Claim("phone", pEntity.NuCellPhone),
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
                expires_in = _tokenSettings.ExpireIn,
                token_type = _tokenSettings.Type
            };
            return tokenWs;
        }

        public TokenDTO Login(UserDTO pEntity)
        {
            UserEntity entity = _userService.ReadByMail(pEntity.UserName);
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            entity.NuAuthAttemptsFail += 1;
            if ((entity.NuAuthAttemptsFail >= _tokenSettings.NuAuthAttempts) && (!entity.Password.Equals(pEntity.Password)))
            {
                _userService.Block(entity);
                throw new UnauthorizedException("Usuário bloqueado!");
            }
            if (entity.IsBlock) throw new UnauthorizedException("Usuário bloqueado!");
            if (!entity.IsActive) throw new UnauthorizedException("Usuário inátivo!");
            if (!entity.Password.Equals(pEntity.Password)) throw new UnauthorizedException("Senha incorreta!");
            return this.GenerateToken(entity);
        }

        public TokenDTO Refresh(TokenDTO pEntity)
        {
            UserEntity entity = null;
            //UserEntity entity = _userService.ReadById(pId);
            return this.GenerateToken(entity);
        }
        #endregion
    }
}