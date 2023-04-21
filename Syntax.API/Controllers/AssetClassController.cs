using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syntax.API.Context;
using Syntax.API.DAL;
using Syntax.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]

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
            return _assetClassDao.List();
        }

        // GET api/<AssetClassController>/5
        [HttpGet("{id}")]
        public AssetClass GetAssetById(int id)
        {
            return _assetClassDao.FindById(id);
        }

        // POST api/<AssetClassController>
        [HttpPost]
        public IActionResult CreateAssetClass(AssetClass assetClass)
        {
            var response = new ErrorResponse();


            // Validação do nome
            if (string.IsNullOrWhiteSpace(assetClass.Name))
            {
                response.Errors.Add("Erro: O nome não pode estar em branco.");
            }
            else if (_assetClassDao.FindByString(assetClass.Name))
            {
                response.Errors.Add("Erro: Já existe uma classe de ativos com esse nome.");
            }

            // Validação da descrição
            if (string.IsNullOrWhiteSpace(assetClass.Description))
            {
                response.Errors.Add("Erro: A descrição não pode estar em branco.");
            }

            // Validação do ícone
            if (string.IsNullOrWhiteSpace(assetClass.Icon))
            {
                response.Errors.Add("Erro: O ícone não pode estar em branco.");
            }
         
            // Se houver erros, retorna um BadRequest com a resposta contendo os erros
            if (response.Errors.Count > 0)
            {
                return BadRequest(response);
            }

            // Se não houver erros, salva a classe de ativos e retorna um Ok
            _assetClassDao.Operation(assetClass, OperationType.Added);
            return Ok(assetClass);
        }

        // PUT api/<AssetClassController>/5
        [HttpPut]
        public IActionResult EditAssetClass(AssetClass assetClass)
        {
            var response = new ErrorResponse();

            if (string.IsNullOrWhiteSpace(assetClass.Name))
            {
                response.Errors.Add("Erro: o campo Name não pode estar em branco.");
            }

            if (string.IsNullOrWhiteSpace(assetClass.Description))
            {
                response.Errors.Add("Erro: o campo Description não pode estar em branco.");
            }

            if (string.IsNullOrWhiteSpace(assetClass.Icon))
            {
                response.Errors.Add("Erro: o campo Icon não pode estar em branco.");
            }

            if (response.Errors.Count > 0)
            {
                return BadRequest(response);
            }

            try
            {
                _assetClassDao.Operation(assetClass, OperationType.Modified);
                return Ok(assetClass);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }


        // DELETE api/<AssetClassController>/5
        [HttpDelete]
        [Authorize]
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

        }// DELETE api/<AssetClassController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteAssetClassById(int id)
        {
            try
            {
                var assetClass =_assetClassDao.FindById(id);

                _assetClassDao.Operation(assetClass!, OperationType.Deleted);
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
