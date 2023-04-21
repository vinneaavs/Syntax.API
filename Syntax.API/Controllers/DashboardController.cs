using Microsoft.AspNetCore.Mvc;
using Syntax.API.Context;

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("TCbyMonth")]
        public IActionResult GetTransactionsByMonth()
        {
            var transactionsByMonth = _context.TransactionClasses
                .GroupBy(t => new { Month = t. .CreatedDate.Month, Year = t.CreatedDate.Year })
                .Select(g => new { MonthYear = $"{g.Key.Month}/{g.Key.Year}", Count = g.Count() })
                .ToList();
            return Ok(transactionsByMonth);
        }
    }
}
