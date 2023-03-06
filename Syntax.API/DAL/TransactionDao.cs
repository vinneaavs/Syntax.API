using Syntax.API.Context;
using Syntax.API.Models;

namespace Syntax.API.DAL
{
    public class TransactionDao:GenericOp<Transaction>
    {
        public TransactionDao(ApplicationDbContext context) : base(context) { }
    }
}
