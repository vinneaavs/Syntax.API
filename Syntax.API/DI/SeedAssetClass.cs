using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syntax.API.Context;
using Syntax.API.Models;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Syntax.API.DI
{
    public class SeedAssetClass
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SeedAssetClass> _logger;

        public SeedAssetClass(ApplicationDbContext context, ILogger<SeedAssetClass> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task CreateAssetClassesAsync()
        {
            var assetClasses = new List<AssetClass>
            {
                new AssetClass
                {
                    Name = "Ação",
                    Description = "Ações listadas na bolsa no Brasil",
                    Icon = "~/Assets/action.svg"
                },
                new AssetClass
                {
                    Name = "Fundo Imobiliário",
                    Description = "FIIs listados na bolsa do Brasil",
                    Icon = "~/Assets/fundimob.svg"
                },
                new AssetClass
                {
                    Name = "Stock",
                    Description = "Ação listada na bolsa Americana",
                    Icon = "~/Assets/action.svg"
                },
                new AssetClass
                {
                    Name = "ETF(BR)",
                    Description = "ETF listada na bolsa Brasileira",
                    Icon = "~/Assets/action.svg"
                },
                new AssetClass
                {
                    Name = "ETF(EUA)",
                    Description = "ETF listada na bolsa Americana",
                    Icon = "~/Assets/action.svg"
                }
            };

            foreach (var assetClass in assetClasses)
            {
                if (!_context.AssetsClasses.Any(ac => ac.Name == assetClass.Name))
                {
                    _context.AssetsClasses.Add(assetClass);
                }
            }
            _logger.LogInformation("Asset classes were seeded.");

            return _context.SaveChangesAsync();
            
        }
    }
}
