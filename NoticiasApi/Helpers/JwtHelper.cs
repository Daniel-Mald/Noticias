using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoticiasApi.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;
        public JwtHelper(IConfiguration conf)
        {
            _configuration = conf;
        }
        public string GetToken(string username , string role, List<Claim> claims)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var issuer = _configuration.GetSection("JWTBearer").GetValue<string>("Issuer")??"";
            var secret = _configuration.GetSection("JWTBearer").GetValue<string>("Secret");
            var audience = _configuration.GetSection("JWTBearer").GetValue<string>("Audience")??"";

            List<Claim> basicas = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Iss , issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience),

            };
            basicas.AddRange(claims);
            //new SigningCredentials
            JwtSecurityToken jwtSecurity = new(issuer,audience,basicas,DateTime.Now, DateTime.Now.AddMinutes(20),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret??"")),
                SecurityAlgorithms.HmacSha256));
            

            return handler.WriteToken(jwtSecurity);

        }
    }
}
