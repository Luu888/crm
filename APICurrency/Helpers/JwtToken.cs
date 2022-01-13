using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICurrency.Helpers
{
    public class JwtToken
    {
        private const string SECRET_KEY = "HSs5fy8dN1xV6R7hpP7qolp71NkE1yg75Z";
        private static readonly SymmetricSecurityKey SIGNING_KEY = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        public static string GenerateJwtToken()
        {
            var cred = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                SIGNING_KEY, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(cred);
            DateTime Expiry = DateTime.UtcNow.AddMinutes(60);
            int ts = (int)(Expiry - new DateTime(1970, 1, 1)).TotalSeconds;
            var payload = new JwtPayload
            {
                { "exp", ts},
                { "iss", "http://localhost:64029/"},
                { "aud", "http://localhost:64029/" }
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);

            return tokenString;


        }
    }
}
