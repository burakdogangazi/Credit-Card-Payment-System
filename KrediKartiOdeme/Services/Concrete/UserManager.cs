using KrediKartiOdeme.Model;
using KrediKartiOdeme.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KrediKartiOdeme.Services.Concrete
{
    public class UserManager : IUserService
    {
        //this user manager is coming from Identity
        private UserManager<IdentityUser> _userManager;

        private IMailService _mailService;

        private IConfiguration _configuration;

        public UserManager(UserManager<IdentityUser> userManager, IConfiguration configuration, IMailService mailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {

            var alreadyRegistered = await _userManager.FindByEmailAsync(model.Email);

            if (alreadyRegistered != null)
            {
                return new UserManagerResponse
                {
                    Message = "This user is already registered",
                    IsSuccess = false,
                };
            }


            if (model == null)
            {
                throw new NullReferenceException("Register model is null");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm Password doesn't match the password ",
                    IsSuccess = false,
                };
            }



            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
            };


            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {

                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                var validEmailToken = EncodeToken(confirmEmailToken);

                string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to TSE Credit Card Payment System</h1>" +
                    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };




        }


        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid password.",
                    IsSuccess = false,
                };
            }

            var claims = new[]
            {
                new Claim("Email",model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AuthSettings:Key").Value));

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("AuthSettings:Issuer").Value,
                audience: _configuration.GetSection("AuthSettings:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));


            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "User not found.",
                    IsSuccess = false,
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            }

            return new UserManagerResponse
            {
                Message = "Email did not confirm.",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };


        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email); ;
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user associated with email",
                    IsSuccess = false
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var validToken = EncodeToken(token);

            string url = $"{_configuration["AppUrl"]}/ResetPassword?email={email}&token={validToken}";

            await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset yout password</h1>" +
               $"<p>To reset your password <a href='{url}'>Click here</a></p>");

            return new UserManagerResponse
            {
                Message = "Reset password URL has been sent to the email successfully!",
                IsSuccess = true
            };

        }

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email); ;
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user associated with email",
                    IsSuccess = false
                };
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Password doesn't match its confirmation",
                    IsSuccess = false
                };
            }

            string normalToken = DecodeToken(model.Token);


            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "Something went wrong!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }


        public string EncodeToken(string token)
        {
            var encodedToken = Encoding.UTF8.GetBytes(token);

            string validToken = WebEncoders.Base64UrlEncode(encodedToken);

            return validToken;

        }

        public string DecodeToken(string token)
        {
            var decodedToken = WebEncoders.Base64UrlDecode(token);

            string normalToken = Encoding.UTF8.GetString(decodedToken);

            return normalToken;
        }

    }
}
