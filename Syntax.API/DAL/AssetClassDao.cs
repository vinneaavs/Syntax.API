﻿using Syntax.API.Context;
using Syntax.API.Models;

namespace Syntax.API.DAL
{
    public class AssetClassDao : GenericOp<AssetClass>
    {
        public AssetClassDao(ApplicationDbContext context) : base(context) { }
    }
}
