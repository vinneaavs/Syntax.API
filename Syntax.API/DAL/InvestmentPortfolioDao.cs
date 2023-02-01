using Syntax.Models;

namespace Syntax.API.DAL
{
    public class InvestmentPortfolioDao:GenericOp<Portfolio>
    {
        public InvestmentPortfolioDao(ApplicationDbContext context) : base(context) { }
    }
}
