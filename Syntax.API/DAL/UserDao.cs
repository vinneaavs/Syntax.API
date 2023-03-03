using Syntax.API.Models;
using Syntax.Auth.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUser = Syntax.Auth.Data.ApplicationUser;

namespace Syntax.API.DAL
{

    public class UserDao : GenericIdentity<ApplicationUser>
    {
        public UserDao(IdentityContext context) : base(context) { }
    }

}
