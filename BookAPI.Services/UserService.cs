using BookAPI.Common;
using BookAPI.Common.Entities;
using BookAPI.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookAPI.Services
{
    public class UserService : IUserService
    {
        private List<User> users = new List<User>();
        private readonly AuthSettings authSettings;
        private readonly byte[] salt;

        public UserService(IOptions<AuthSettings> appSettings)
        {
            authSettings = appSettings.Value;
            salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            users.Add(new User { Id = Guid.NewGuid(), FirstName = "Test", LastName = "User", UserName = "User1", PasswordHash = HashPassword("test"), UserRole = "User" });
            users.Add(new User { Id = Guid.NewGuid(), FirstName = "Test 2", LastName = "User 2", UserName = "User2", PasswordHash = HashPassword("test2"), UserRole = "Admin" });
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
                );
        }

        public string Authenticate(string username, string password)
        {
            var user = users.SingleOrDefault(x => x.UserName == username && x.PasswordHash == HashPassword(password));
            return user == null ? null : GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("role", user.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: authSettings.Issuer,
                audience: authSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User GetById(Guid id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }
    }
}
