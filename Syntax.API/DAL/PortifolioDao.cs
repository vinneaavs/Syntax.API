using Syntax.API.Context;
using Syntax.API.Models;
using System.Transactions;

namespace Syntax.API.DAL
{
    public class PortifolioDao:GenericOp<Portfolio>
    {
        public PortifolioDao(ApplicationDbContext context) : base(context) { }
    }
}
