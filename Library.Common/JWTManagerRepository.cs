using AutoMapper.Configuration;
using Library.Common.Dtos;
using Library.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly DateTime expiresTime;
        private readonly IConfiguration iconfiguration;
        private readonly IDatabaseHelper databaseHelper;
        public JWTManagerRepository(IConfiguration iconfiguration, IDatabaseHelper databaseHelper, DateTime expiresTime)
        {
            this.iconfiguration = iconfiguration;
            this.databaseHelper = databaseHelper;
            this.expiresTime = expiresTime;
        }
        public Tokens Authenticate(string email, string password)
        {
            var user = databaseHelper.Users.GetList(1, new DbQueryParameter
            {
                Value = email,
                Name = nameof(User.Email),
                CompareOperator = CompareOperator.Equal,
                LogicOperator = LogicOperator.AND
            },
            new DbQueryParameter
            {
                Value = password,
                CompareOperator = CompareOperator.Equal,
                Name = nameof(User.Password)
            }).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = expiresTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
