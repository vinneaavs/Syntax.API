using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

public class TokenValidationFilter : IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TokenValidationFilter> _logger;

    public TokenValidationFilter(IConfiguration configuration, ILogger<TokenValidationFilter> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        var request = context.HttpContext.Request;
        var isLocal = request.Host.Host == "localhost" && request.Host.Port == 5069;

        if (isLocal)
        {
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtOptions:SecurityKey"]);
            tokenHandler.ValidateToken(token.ToString().Replace("Bearer ", ""), new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            context.HttpContext.Items["UserId"] = userId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to validate token");
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
