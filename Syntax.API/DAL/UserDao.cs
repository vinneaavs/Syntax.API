using Syntax.Models;

namespace Syntax.API.DAL
{
    public class UserDao : GenericOp<User>
    {
        public UserDao(ApplicationDbContext context) : base(context) { }
    }
}
