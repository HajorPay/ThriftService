using HajorPay.ThriftService.Application.Abstractions.Identity;
using HajorPay.ThriftService.Application.Data.Interfaces;
using HajorPay.ThriftService.Application.DTOs.User;
using HajorPay.ThriftService.Application.Features.User.Commands;
using HajorPay.ThriftService.Domain.Constants;
using HajorPay.ThriftService.Domain.Entities.Auth;
using HajorPay.ThriftService.Infrastructure.Contexts;
using HajorPay.ThriftService.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HajorPay.ThriftService.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserCommand command)
        {
            var user = new ApplicationUser
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
                Email = command.EmailAddress,
                UserName = command.UserName,
                BVN = command.BVN,
                NIN = command.NIN,
                OptInForSMS = command.OptInForSMS,
                OptInForEmail = command.OptInForEmail,
                RegisteredByAdmin = command.RegisteredByAdmin
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, DomainConstants.Role.User);
            if (!roleResult.Succeeded)
            {
                throw new Exception("Assigning user role failed.");
            }
            //TODO: Rollback if role creation failed
            //Send Email Confirmation
            return new UserDto
            {
                Id = user.Id,
                EmailAddress = user.Email!,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<LoginDto> LoginAsync(LoginUserCommand command)
        {
            var user = await _userManager
                .Users
                .FirstOrDefaultAsync(u => u.UserName == command.Username || u.Email == command.Username || u.PhoneNumber == command.Username);

            if (user == null) 
            {
                throw new Exception("Invalid Username or Password"); //TODO: Replace with a response with the correct status code
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false); //TODO: lock after a number of attempts

            if (!result.Succeeded)
            {
                throw new Exception("Username not found and/or password incorrect");
            }

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = _tokenService.CreateRefreshToken(),
                Created = DateTime.UtcNow,
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            _dbContext.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();


            return new LoginDto
            {
                Id = user.Id,
                EmailAddress = user.Email!,
                Token = _tokenService.CreateToken(user),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<RefreshTokenDto> RefreshTokenAsync(RefreshTokenCommand command)
        {
            //TODO: URGENT Finish Milan
            //TODO: Re-implement this method to validate refresh token
            var refreshToken = await _dbContext.RefreshTokens
                //.Include(x => x.User)
                .FirstOrDefaultAsync(rt => rt.Token == command.RefreshToken);

            if (refreshToken == null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                throw new Exception("The refresh token has expired");
            }

            var user = await _userManager
                .Users
                .FirstOrDefaultAsync(u => u.Id == refreshToken.UserId);

            string accessToken = _tokenService.CreateToken(user!);
            refreshToken.Token = _tokenService.CreateRefreshToken();
            refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

            await _dbContext.SaveChangesAsync();

            return new RefreshTokenDto
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
