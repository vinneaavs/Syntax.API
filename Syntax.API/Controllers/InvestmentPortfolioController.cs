using Microsoft.AspNetCore.Mvc;
using Syntax.API.DAL;
using Syntax.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentPortfolioController : ControllerBase
    {
        private readonly InvestmentPortfolioDao investmentPortfolioDao;
        public InvestmentPortfolioController(ApplicationDbContext context)
        {
            investmentPortfolioDao = new InvestmentPortfolioDao(context);
        }
        // GET: api/<InvestmentPortfolioController>
        [HttpGet]
        public IEnumerable<InvestmentPortfolio> Get()
        {
            return investmentPortfolioDao.List();
        }

        // GET api/<InvestmentPortfolioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvestmentPortfolioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InvestmentPortfolioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvestmentPortfolioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
