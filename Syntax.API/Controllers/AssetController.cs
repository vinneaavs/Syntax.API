using Microsoft.AspNetCore.Mvc;
using Syntax.API.DAL;
using Syntax.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly AssetDao _assetDao;
        private AssetController(ApplicationDbContext _context)
        {
            _assetDao = new AssetDao(_context);
        }
        // GET: api/<AssetController>
        [HttpGet]
        public IEnumerable<Asset> GetAssets()
        {
            return _assetDao.List().ToList();
        }

        // GET api/<AssetController>/5
        [HttpGet("{id}")]
        public Asset GetAsset(int id)
        {
            return _assetDao.FindById(id);
        }

        // POST api/<AssetController>
        [HttpPost]
        public Asset CreateAsset(Asset asset)
        {
            _assetDao.Operation(asset, OperationType.Added);
            return asset;
        }

        // PUT api/<AssetController>/5
        [HttpPut("{id}")]
        public Asset EditAsset(Asset asset)
        {
            _assetDao.Operation(asset, OperationType.Modified);
            return asset;
        }

        // DELETE api/<AssetController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAsset(Asset asset)
        {
            try
            {
                _assetDao.Operation(asset, OperationType.Deleted);
                return Ok("Deletado com sucesso!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
