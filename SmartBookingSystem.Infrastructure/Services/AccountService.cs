using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartBookingSystem.Application.Constants;
using SmartBookingSystem.Application.DTOs.Account;
using SmartBookingSystem.Application.Interfaces;
using SmartBookingSystem.Domain.Entities;
using SmartBookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole<Guid>> roleManager,IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new Exception("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
                throw new Exception("User has no assigned role.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                claims: claims,
                signingCredentials: creds
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Role = roles.FirstOrDefault(),
                UserId = user.Id,
                UserName = user.UserName
            };

        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, request.Role);
            if (request.Role.Equals(Roles.Provider, StringComparison.OrdinalIgnoreCase))
            {
                var provider = new Provider
                {
                    ApplicationUserId = user.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };
                await _unitOfWork.Providers.AddAsync(provider);
            }
            else if (request.Role.Equals(Roles.Customer, StringComparison.OrdinalIgnoreCase))
            {
                var customer = new Customer
                {
                    ApplicationUserId = user.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                };
                await _unitOfWork.Customers.AddAsync(customer);
            }
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving changes: " + ex.InnerException?.Message ?? ex.Message);
            }
            return "Registration successful";
        }

        public async Task<string> AdminCreateProviderAsync(AdminCreateProviderRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, Roles.Provider);

            var provider = new Provider
            {
                ApplicationUserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Description = request.Description
            };

            await _unitOfWork.Providers.AddAsync(provider);
            await _unitOfWork.SaveChangesAsync();

            return provider.Id.ToString();
        }

    }
}
