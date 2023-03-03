using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syntax.Application.Interfaces.Services;
using Syntax.Application.DTOs.Request;
using Syntax.Application.DTOs.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Syntax.Auth.Configurations;
using Syntax.Auth.Data;

namespace Syntax.Auth.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IdentityContext _context;

        public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions, IdentityContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _context = context;
        }
        public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegisterRequest)
        {

            var applicationUser = new ApplicationUser
            {
                Email = userRegisterRequest.Email,
                UserName = userRegisterRequest.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(applicationUser, userRegisterRequest.Password);
            if (result.Succeeded)
            {
                //NÃO FUNCIONA COM USER PERSONALIZADO                
                //await _userManager.SetLockoutEnabledAsync(applicationUser, false);

                var user =  _userManager.Users.FirstOrDefault(x => x.IdApp == applicationUser.IdApp);
                if (user != null)
                {
                    user.LockoutEnabled = user.LockoutEnabled = false;
                    user.IdApp = user.IdApp;
                    
                    _context.SaveChanges();
                }


            }
            var userRegisterResponse = new UserRegisterResponse(result.Succeeded);
            if (!result.Succeeded && result.Errors.Count() > 0)
            {
                userRegisterResponse.AddErrors(result.Errors.Select(e => e.Description));

            }
            return userRegisterResponse;
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest userLoginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginRequest.Email, userLoginRequest.Password, false, false);
            if (result.Succeeded)
            {
                return await TokenGenerate(userLoginRequest.Email);
            }
            var userLoginResponse = new UserLoginResponse(result.Succeeded);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    userLoginResponse.AddError("Conta Bloqueada !");
                }
                else if (result.IsNotAllowed)
                {
                    userLoginResponse.AddError("Conta Sem permissão para essa função !");
                }
                else if (result.RequiresTwoFactor)
                {
                    userLoginResponse.AddError($"TwoFactor Authentication required.");
                }
                else
                {
                    userLoginResponse.AddError("Login ou senha inválidos !");
                }
            }
            return userLoginResponse;
        }

        private async Task<UserLoginResponse> TokenGenerate(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            var tokenClaims = await GetClaims(user);
            var dateExpire = DateTime.Now.AddSeconds(_jwtOptions.Expiration);
            var jwt = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: dateExpire,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new UserLoginResponse
                (
                    success: true,
                    token: token,
                    expirationDate: dateExpire

                );
        }

        private async Task<IList<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var r in roles)
            {
                claims.Add(new Claim("roles", r));
            }
            return claims;
        }

    }
}


