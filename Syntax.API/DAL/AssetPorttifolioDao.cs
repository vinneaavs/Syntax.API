using Syntax.Models;

namespace Syntax.API.DAL
{
    public class AssetPorttifolioDao : GenericOp<Portfolio>
    {
        public AssetPorttifolioDao(ApplicationDbContext context) : base(context) { }
    }
}
