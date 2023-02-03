using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;

namespace Syntax.API.DI
{
    public static class DIAuth
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AuthDbContext>(
                   op =>
                    op
                    .UseSqlServer(configuration.GetConnectionString("IdentityConnections")))
                    .BuildServiceProvider();

            return services;

        }
    }
}
