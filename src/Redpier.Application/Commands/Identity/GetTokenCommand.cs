using MediatR;
using Redpier.Application.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Redpier.Application.Commands.Identity
{
    public class GetTokenCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public GetTokenCommandHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Username))
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var identity = new ClaimsIdentity("Bearer");

                var roles = await _userRepository.GetRolesAsync(user.Username);

                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }

                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddHours(Double.Parse(_configuration["BearerToken:Expires"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(_configuration["BearerToken:Secret"])), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }

            throw new UnauthorizedAccessException();
        }
    }
}
