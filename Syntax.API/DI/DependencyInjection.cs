using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;

namespace Syntax.API.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfStructDB(this IServiceCollection services, IConfiguration configuration)
        {
            var provider =
                services
                .AddDbContext<ApplicationDbContext>(
                options =>
                options
                .UseSqlServer(configuration.GetConnectionString("DevConnections")))
            .BuildServiceProvider();

            var context = provider
                .GetRequiredService<ApplicationDbContext>();
            DbInitializer
                .Initialize(context);
            return services;
        }

    }
}
