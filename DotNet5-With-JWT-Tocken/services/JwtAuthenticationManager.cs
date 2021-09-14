using DotNet5_With_JWT_Tocken.Controllers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DotNet5_With_JWT_Tocken.services
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string tokenKey;

        public JwtAuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }

        public List<UserCred> Users = new List<UserCred>
        {
            new UserCred{ UserName="Ajay", Password="123"},
            new UserCred{UserName="Golu", Password="111"}
        }; 
         
        public string Authenticate(string UserName, string Password)
        {
            if (!Users.Any(u => u.UserName.Equals(UserName) && u.Password.Equals(Password)))
            {
                return null;
            }
            var tokenHadler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("8x/A?D(G+KbPeShVmYp3s6v9y$B&E)H@");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, UserName)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHadler.CreateToken(tokenDescriptor);
            return tokenHadler.WriteToken(token);
        }
    }
}
