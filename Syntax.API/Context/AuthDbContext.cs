using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Syntax.API.Context
{
    public class AuthDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> op) : base(op) { }
    }
}
