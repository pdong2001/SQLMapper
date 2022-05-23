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
        public Tokens Authenticate(string email, string password, params DbQueryParameter[] userDatas)
        {
            var userQueryParameters = new List<DbQueryParameter>();

            userQueryParameters.Add(new DbQueryParameter
            {
                Value = email,
                Name = nameof(User.Email),
                CompareOperator = CompareOperator.Equal,
                LogicOperator = LogicOperator.AND
            });

            userQueryParameters.Add(new DbQueryParameter
            {
                Value = password,
                CompareOperator = CompareOperator.Equal,
                Name = nameof(User.Password),
                LogicOperator = LogicOperator.AND
            });
            userQueryParameters.AddRange(userDatas);
            if (userQueryParameters.Any(p => p.LogicOperator == LogicOperator.OR))
            {
                throw new Exception("Can not use OR operator to find user.");
            }
            var user = databaseHelper.Users.GetList(1, new DbQueryParameterGroup(LogicOperator.AND, userQueryParameters.ToArray())).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
                }),
                Expires = expiresTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
