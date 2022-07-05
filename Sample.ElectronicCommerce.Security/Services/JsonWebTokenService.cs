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
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class JsonWebTokenService
    {
        #region Variables

        private readonly TokenSettings _tokenSettings;

        private readonly UserService _userService;
        #endregion

        #region Constructor
        public JsonWebTokenService(
            IOptions<TokenSettings> tokenSettings,
            UserService userService
        )
        {
            _tokenSettings = tokenSettings.Value;
            _userService = userService;
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
                new Claim("phone", pEntity.Phone),
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
            entity.NuAuthAttempts += 1;
            if ((entity.NuAuthAttempts >= _tokenSettings.NuAuthAttempts) && (!entity.Password.Equals(pEntity.Password)))
            {
                _userService.ForgotPassword(new ForgotPasswordDTO() { UserName = pEntity.UserName });
                throw new UnauthorizedException("Usuário bloqueado!");
            }
            if (entity.Status.Equals(AppConstant.StatusBlock)) throw new UnauthorizedException("Usuário bloqueado!");
            if (entity.Status.Equals(AppConstant.StatusInactive)) throw new UnauthorizedException("Usuário inátivo!");
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