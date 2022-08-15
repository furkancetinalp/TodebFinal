using Business.Abstract;
using Business.Configuration.Auth;
using Business.Configuration.Helper;
using Bussines.Configuration.Response;
using DAL.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        // distributed cache
        private readonly IDistributedCache _distributedCache;

        public AuthService(IUserRepository repository, IConfiguration configuration, IDistributedCache distributedCache)
        {
            _repository = repository;
            _configuration = configuration;
            _distributedCache = distributedCache;
        }
        //Method of verifying password
        public CommandResponse VerifyPassword(string email, string password)
        {
            var user = _repository.GetUserWithPassword(email);
            if (user is null)
                return new CommandResponse { Message = "invalid E-mail!! Login failed!!!", Status = false };

            if (HashHelper.VerifyPasswordHash(password,user.UserPassword.PasswordHash,user.UserPassword.PasswordSalt))
            {
                return new CommandResponse { Message = "login successful", Status = true };
            }
            return new CommandResponse { Message = "login failed!!!", Status = false };
        }

        //Login method
        public AccessToken Login(string email, string password)
        {
            #region Token
            var verifypassword =VerifyPassword(email, password);
            var user = _repository.Get(x => x.Mail == email);

            //auth addition
            if (verifypassword.Status)
            {
                var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOption>();

                var expireDate = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(ClaimTypes.GivenName, user.Name),
                    new Claim(ClaimTypes.Role,user.UserRole.ToString()),
                    new Claim("ForCache",StringHelper.CreateCacheKey(user.Name,user.Id))
                };
                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenOptions.Issuer,
                    audience: tokenOptions.Audience,
                    claims: claims,
                    expires: expireDate,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                );

                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                #endregion

                #region Cache

                //User ıd+user key =>token will be saved 
                _distributedCache.SetString($"USR_{user.Id}", token);
                #endregion

                return new AccessToken()
                {
                    Token = token,
                    ExpireDate = expireDate
                };
            }
            return new AccessToken()
            {
            };
        }
    }
}
