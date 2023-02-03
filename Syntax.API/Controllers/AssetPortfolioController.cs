using Microsoft.AspNetCore.Mvc;
using Syntax.API.Context;
using Syntax.API.DAL;
using Syntax.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetPortfolioController : ControllerBase
    {
        private readonly AssetPorttifolioDao _assetPortifolioDao;
        public AssetPortfolioController(ApplicationDbContext _context)
        {
            _assetPortifolioDao = new AssetPorttifolioDao(_context);
        }
        // GET: api/<InvestmentPortfolioController>
        [HttpGet]
        public IEnumerable<AssetPortfolio> GetAssetsPortfolios()
        {
            return _assetPortifolioDao.List().ToList();
        }

        // GET api/<InvestmentPortfolioController>/5
        [HttpGet("{id}")]
        public AssetPortfolio GetAssetPortfolio(int id)
        {
            return _assetPortifolioDao.FindById(id);
        }

        // POST api/<InvestmentPortfolioController>
        [HttpPost]
        public AssetPortfolio CreateAssetPortfolio(AssetPortfolio assetPortfolio)
        {
            _assetPortifolioDao.Operation(assetPortfolio, OperationType.Added);
            return assetPortfolio;

        }

        // PUT api/<InvestmentPortfolioController>/5
        [HttpPut("{id}")]
        public AssetPortfolio EditAssetPortfolio(AssetPortfolio assetPortfolio)
        {
            _assetPortifolioDao.Operation(assetPortfolio, OperationType.Added);
            return assetPortfolio;
        }

        // DELETE api/<InvestmentPortfolioController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAssetPortfolio(AssetPortfolio assetPortfolio)
        {
            try
            {
                _assetPortifolioDao.Operation(assetPortfolio, OperationType.Deleted);
                return Ok("Asset Portfolio excluido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
