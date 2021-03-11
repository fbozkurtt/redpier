using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Redpier.Application.Common.Interfaces.Identity;
using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public IdentityService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<bool> AddToRoleAsync(string username, string roleName)
        {
            return await _userRepository.AddToRoleAsync(username, roleName);
        }

        public async Task<IList<Role>> GetRoles(string username)
        {
            var user = await _userRepository.GetAsync(username);

            return user.Roles.ToList();
        }

        public async Task<string> GetTokenAsync(string username, string password)
        {
            var user = await _userRepository.GetAsync(username);

            if (user != null && ValidateAsync(user, password).Result)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var identity = new ClaimsIdentity("Bearer");

                if(user.Roles != null)
                {
                    foreach (var role in user.Roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddHours(Double.TryParse(_configuration["BearerToken:Expires"], out double expires) ? expires : 1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(_configuration["BearerToken:Secret"])), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }

            throw new UnauthorizedAccessException();
        }

        public async Task<bool> IsInRoleAsync(string username, string roleName)
        {
            var user = await _userRepository.GetAsync(username);

            return user.Roles.Any(w => w.Name.Equals(roleName));
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return await Task.Run(() =>
            {
                return user.Roles.Any(w => w.Name.Equals(roleName));
            });
        }

        public async Task<bool> ValidateAsync(User user, string password)
        {
            return await Task.Run(() =>
            {
                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            });
        }
    }
}
