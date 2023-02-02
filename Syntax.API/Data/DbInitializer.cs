using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Syntax.Models;

namespace Syntax.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
