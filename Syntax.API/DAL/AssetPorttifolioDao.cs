using Syntax.Models;

namespace Syntax.API.DAL
{
    public class AssetPorttifolioDao : GenericOp<AssetPortfolio>
    {
        public AssetPorttifolioDao(ApplicationDbContext context) : base(context) { }
    }
}
