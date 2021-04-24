using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoodOrder.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FoodOrder.Shared.Dtos;

namespace FoodOrder.Application.Infrastructure.Security
{
    public class JWTGenerator :IJWTGenerator
    {
        public IConfiguration Configuration { get; }

        public JWTGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public UserLoginDto CreateToken(Domain.Entities.Users user)
        {
            UserLoginDto userLogin = new();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));

            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.UserData,user.Id.ToString())
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenExpires = int.Parse(Configuration["TokenExpires"]);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(tokenExpires),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userLogin.Token = tokenHandler.WriteToken(token);
            return userLogin;
        }
    }
}