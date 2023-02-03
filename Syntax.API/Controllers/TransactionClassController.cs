using Microsoft.AspNetCore.Mvc;
using Syntax.API.DAL;
using Syntax.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionClassController : ControllerBase
    {
        private readonly TransactionClassDao _transactionClassDao;
        public TransactionClassController(ApplicationDbContext _context)
        {
            _transactionClassDao = new TransactionClassDao(_context);
        }
        // GET: api/<TransactionClassController>
        [HttpGet]
        public IEnumerable<TransactionClass> GetTransactionClasses()
        {
            return _transactionClassDao.List().ToList();
        }

        // GET api/<TransactionClassController>/5
        [HttpGet("{id}")]
        public TransactionClass GetTransactionClass(int id)
        {
            return _transactionClassDao.FindById(id);
        }

        // POST api/<TransactionClassController>
        [HttpPost]
        public TransactionClass CreateTransactionClass(TransactionClass transactionClass)
        {
            _transactionClassDao.Operation(transactionClass, OperationType.Added);
            return transactionClass;
        }

        // PUT api/<TransactionClassController>/5
        [HttpPut]
        public TransactionClass EditTransactionClass(TransactionClass transactionClass)
        {
            _transactionClassDao.Operation(transactionClass, OperationType.Modified);
            return transactionClass;
        }

        // DELETE api/<TransactionClassController>/5
        [HttpDelete]
        public IActionResult Delete(TransactionClass transactionClass)
        {
            try
            {
                _transactionClassDao.Operation(transactionClass, OperationType.Deleted);
                return Ok("TransactionClass excluida com sucesso !");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
