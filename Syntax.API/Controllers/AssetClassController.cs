using Microsoft.AspNetCore.Mvc;
using Syntax.API.DAL;
using Syntax.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetClassController : ControllerBase
    {

        private readonly AssetClassDao _assetClassDao;
        public AssetClassController(ApplicationDbContext _context)
        {
            _assetClassDao = new AssetClassDao(_context);
        }
        // GET: api/<AssetClassController>
        [HttpGet]
        public IEnumerable<AssetClass> GetAssets()
        {
            return _assetClassDao.List().ToList();
        }

        // GET api/<AssetClassController>/5
        [HttpGet("{id}")]
        public AssetClass GetAssetById(int id)
        {
            return _assetClassDao.FindById(id);
        }

        // POST api/<AssetClassController>
        [HttpPost]
        public AssetClass CreateAssetClass(AssetClass assetClass)
        {
            _assetClassDao.Operation(assetClass, OperationType.Added);
            return assetClass;
        }

        // PUT api/<AssetClassController>/5
        [HttpPut]
        public AssetClass EditAssetClass(AssetClass assetClass)
        {
            _assetClassDao.Operation(assetClass, OperationType.Modified);
            return assetClass;
        }

        // DELETE api/<AssetClassController>/5
        [HttpDelete]
        public IActionResult DeleteAssetClass(AssetClass assetClass)
        {
            try
            {
                _assetClassDao.Operation(assetClass, OperationType.Deleted);
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
