using BoscoAFH.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace BoscoAFH.Base
{
    public interface IJwtTokenGenerator
    {

        string GenerateToken(UserDetailDTO userDetail);

        string GenerateRefreshToken();
    }

    public class JwtTokenGenerator(IOptions<JWTSetting> jwtSetting) : IJwtTokenGenerator
    {
        public string GenerateToken(UserDetailDTO userDetail)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(jwtSetting.Value.SecurityKey);
            var claimsDict1 = new Dictionary<string, object>
            {
                { Constant.SessionField.RoleId, CommonMethods.EncryptValue(userDetail.RoleId.ToString()) },
                { Constant.SessionField.UserId, CommonMethods.EncryptValue(userDetail.UserId.ToString()) },
                { Constant.SessionField.FullName, CommonMethods.EncryptValue(userDetail.FullName.ToString()) }
            };
            var claimsDict = new Dictionary<string, object>
            {
                { Constant.SessionField.RoleId, (userDetail.RoleId.ToString()) },
                { Constant.SessionField.UserId, (userDetail.UserId.ToString()) },
                { Constant.SessionField.FullName,  (userDetail.FullName.ToString()) }
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtSetting.Value.Audience,
                Issuer = jwtSetting.Value.Issuer,
                Claims = claimsDict,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
