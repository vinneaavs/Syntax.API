using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/<HomeController>
        [AllowAnonymous]
        [HttpGet]
        public string Get()
        {
            string a = "\t\t\t\t\t\t\t\t\t\t\t\t\t ____              _         " +
                "        _    ____ ___ \r\n\t\t\t\t\t\t\t\t\t\t\t\t\t/ ___| _   _" +
                " _ __ | |_" +
                " __ ___  __   / \\  |  _ \\_ _|\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\\___ " +
                "\\| | | | '_ \\| __/ _` \\ \\/ /  / _ \\ | |_) | | \r\n\t\t\t\t\t\t\t\t" +
                "\t\t\t\t\t ___) | |_| | | | | || (_| |>  <  / ___ \\|  __/| | \r\n" +
                "\t\t\t\t\t\t\t\t\t\t\t\t\t|____/ \\__, |_| |_|\\__\\__,_/_/\\_\\/_/" +
                "   \\" +
                "_\\_|  |___|\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t       |___/             " +
                "                          \r\n";
            return a;
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
