﻿using Syntax.API.Context;
using Syntax.Models;

namespace Syntax.API.DAL
{
    public class AssetDao : GenericOp<Asset>
    {
        public AssetDao(ApplicationDbContext context) : base(context) { }
    }
}
