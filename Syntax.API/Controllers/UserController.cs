using Microsoft.AspNetCore.Mvc;
using Syntax.API.DAL;
using Syntax.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDao _userDao;
        public UserController(ApplicationDbContext _context)
        {
            _userDao = new UserDao(_context);
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _userDao.List().ToList();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User GetUserById(int id)
        {
            return _userDao.FindById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public User CreateUser(User user)
        {
            _userDao.Operation(user, OperationType.Added);
            return user;
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public User EditUser(User user)
        {
            _userDao.Operation(user, OperationType.Modified);
            return user;
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        public IActionResult Delete(User user)
        {
            try
            {
                _userDao.Operation(user, OperationType.Deleted);
                return Ok($"Usuario {user.Name} - {user.Id} Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
