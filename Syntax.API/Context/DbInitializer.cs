using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Syntax.API.Context
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
