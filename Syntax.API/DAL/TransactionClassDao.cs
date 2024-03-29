﻿using Syntax.API.Context;
using Syntax.API.Models;

namespace Syntax.API.DAL
{
    public class TransactionClassDao : GenericOp<TransactionClass>
    {
        public TransactionClassDao(ApplicationDbContext context) : base(context) { }
    }
}
