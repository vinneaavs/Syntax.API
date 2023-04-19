using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Syntax.Auth.Data;

namespace Syntax.API.Context
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
        public static void Initialize(IdentityContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
