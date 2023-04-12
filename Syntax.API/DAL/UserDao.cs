using Syntax.API.Models;
using Syntax.Auth.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax.API.DAL
{
    public class UserDao : GenericIdentity<ApplicationUser>
    {
        public UserDao(IdentityContext context, UserManager<ApplicationUser> userManager)
            : base(context, userManager)
        {
        }
    }
}
