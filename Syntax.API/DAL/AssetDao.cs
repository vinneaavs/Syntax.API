using Syntax.Models;

namespace Syntax.API.DAL
{
    public class AssetDao : GenericOp<AssetDao>
    {
        public AssetDao(ApplicationDbContext context) : base(context) { }
    }
}
