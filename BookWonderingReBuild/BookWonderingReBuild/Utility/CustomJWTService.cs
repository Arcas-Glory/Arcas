using BookWonderingReBuild.Model.Entitys;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookWonderingReBuild.Utility
{
    public class CustomJWTService : ICustomJWTService
    {
        private readonly JWTTokenOptions _JWTTokenOptions;
        public CustomJWTService(IOptionsMonitor<JWTTokenOptions> _JWTTokenOptions)
        {
            this._JWTTokenOptions = _JWTTokenOptions.CurrentValue;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetToken(Users user)
        {
            #region 有效载荷，大家可以自己写，爱写多少写多少；尽量避免敏感信息
            var claims = new[]
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("QQ",user.QQ),
                new Claim("nickName",user.NickName),
                new Claim("UserType",user.UserType.ToString()),
                new Claim("Mobile",user.Mobile.ToString()),
            };

            //需要加密：需要加密key:
            //Nuget引入：Microsoft.IdentityModel.Tokens
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Nuget引入：System.IdentityModel.Tokens.Jwt
            JwtSecurityToken token = new JwtSecurityToken(
             issuer: _JWTTokenOptions.Issuer,
             audience: _JWTTokenOptions.Audience,
             claims: claims,
             expires: DateTime.Now.AddMinutes(10),//设置多少分钟有效期
             signingCredentials: creds);

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
            #endregion

        }
    }
}
