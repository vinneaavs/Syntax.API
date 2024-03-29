﻿using Syntax.API.Context;
using Syntax.API.Models;

namespace Syntax.API.DAL
{
    public class AssetPorttifolioDao : GenericOp<AssetPortfolio>
    {
        public AssetPorttifolioDao(ApplicationDbContext context) : base(context) { }
    }
}
