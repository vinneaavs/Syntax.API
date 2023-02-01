using Syntax.Models;

namespace Syntax.API.DAL
{
    public class InvestmentPortfolioDao:GenericOp<InvestmentPortfolio>
    {
        public InvestmentPortfolioDao(ApplicationDbContext context) : base(context) { }
    }
}
