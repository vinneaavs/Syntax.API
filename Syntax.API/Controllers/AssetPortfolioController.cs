using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;
using Syntax.API.DAL;
using Syntax.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetPortfolioController : ControllerBase
    {
        private readonly AssetPorttifolioDao _assetPortifolioDao;
        private readonly ApplicationDbContext _applicationDbContext;
        public AssetPortfolioController(ApplicationDbContext _context)
        {
            _applicationDbContext = _context;
            _assetPortifolioDao = new AssetPorttifolioDao(_context);
        }
        // GET: api/<InvestmentPortfolioController>
        [HttpGet]
        public IEnumerable<AssetPortfolio> GetAssetsPortfolios()
        {
            return _assetPortifolioDao.List().ToList();
        }
        [HttpGet("user/{idUser}")]
        public IEnumerable<AssetPortfolio> GetAssetsPortfoliosByUser(string idUser)
        {
            var list = _applicationDbContext.AssetPortfolios.Include(x=>x.PortFolioNavigation).Where(x=>x.PortFolioNavigation.IdUser == idUser).ToList();

            return list;
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
            assetPortfolio.CreationDate = DateTime.Now;
            _assetPortifolioDao.Operation(assetPortfolio, OperationType.Added);
            return assetPortfolio;

        }

        // PUT api/<InvestmentPortfolioController>/5
        [HttpPut("{id}")]
        public AssetPortfolio EditAssetPortfolio(AssetPortfolio assetPortfolio)
        {
            _assetPortifolioDao.Operation(assetPortfolio, OperationType.Modified);
            return assetPortfolio;
        }

        // DELETE api/<InvestmentPortfolioController>/5
        [HttpDelete]
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
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteAssetPortifolioById(int id)
        {
            try
            {
                var assetPortifolio = _assetPortifolioDao.FindById(id);

                _assetPortifolioDao.Operation(assetPortifolio!, OperationType.Deleted);
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
