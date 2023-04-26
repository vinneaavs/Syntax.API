using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syntax.API.Context;
using Syntax.API.DAL;
using Syntax.API.Models;
using Syntax.Auth.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly TransactionDao _transactionDao;
        private readonly ApplicationDbContext _applicationDbContext;
        public TransactionController(ApplicationDbContext _context)
        {
            _transactionDao = new TransactionDao(_context);
            _applicationDbContext = _context;
        }


        [HttpGet("{idUser}")]
        public IEnumerable<Transaction> GetTransactionsByUser(string idUser)
        {
            var list = _applicationDbContext.Transactions.Where(x=>x.IdUser == idUser).ToList();
            return list;
        }


        // GET: api/<TransactionController>
        [HttpGet]
        
        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactionDao.List().ToList();
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public Transaction GetTransaction(int id)
        {
            return _transactionDao.FindById(id);
        }

        // POST api/<TransactionController>
        [HttpPost]
        public Transaction CreateTransaction(Transaction transaction)
        {
            _transactionDao.Operation(transaction, OperationType.Added);
            return transaction;

        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public Transaction EditTransaction(Transaction transaction)
        {
            _transactionDao.Operation(transaction, OperationType.Added);
            return transaction;
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete]
        public IActionResult DeleteTransaction(Transaction transaction)
        {
            try
            {
                _transactionDao.Operation(transaction, OperationType.Deleted);
                return Ok($"Transaction {transaction.Id} - {transaction.Description} Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteTransactionById(int id)
        {
            try
            {
                var transaction = _transactionDao.FindById(id);

                _transactionDao.Operation(transaction!, OperationType.Deleted);
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
