using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Syntax.API.Context;
using Syntax.API.DAL;
using Syntax.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly PortifolioDao _portifolioDao;
        private readonly ApplicationDbContext _applicationDbContext;
        public PortfolioController(ApplicationDbContext _context)
        {
            _portifolioDao = new PortifolioDao(_context);
            _applicationDbContext = _context;

        }
        [HttpGet("user/{idUser}")]
        public IEnumerable<Portfolio> GetPortfoliosByUser(string idUser)
        {
            var list = _applicationDbContext.Portfolios.Where(x => x.IdUser == idUser).ToList();
            return list;
        }


        // GET: api/<PortfolioController>
        [HttpGet]
        public IEnumerable<Portfolio> GetPortfolios()
        {
            return _portifolioDao.List().ToList();
        }

        // GET api/<PortfolioController>/5
        [HttpGet("{id}")]
        public Portfolio GetPortfolio(int id)
        {
            return _portifolioDao.FindById(id);
        }

        // POST api/<PortfolioController>
        [HttpPost]
        public Portfolio CreatePortfolio(Portfolio portfolio)
        {
            portfolio.CreationDate = DateTime.Now;
            _portifolioDao.Operation(portfolio, OperationType.Added);
            return portfolio;
        }

        // PUT api/<PortfolioController>/5
        [HttpPut]
        public Portfolio EditPortfolio(Portfolio portfolio)
        {
            _portifolioDao.Operation(portfolio, OperationType.Modified);
            return portfolio;
        }

        // DELETE api/<PortfolioController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePortfolioById(int id)
        {
            try
            {
                var portfolio = _portifolioDao.FindById(id);

                _portifolioDao.Operation(portfolio!, OperationType.Deleted);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpDelete]
        public IActionResult DeletePortfolio(Portfolio portfolio)
        {
            try
            {
                _portifolioDao.Operation(portfolio, OperationType.Deleted);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
