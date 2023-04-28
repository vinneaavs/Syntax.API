using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;
using Syntax.API.Models;
using Syntax.Auth.Data;
using System.Globalization;

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IdentityContext __context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DashboardController(ApplicationDbContext context, IdentityContext context_)
        {
            _context = context;
            __context = context_;
        }

        [HttpGet("UserByMonth")]
        public IActionResult GetUserCreatedByMonth()
        {
            var roles = __context.Roles.Select(r => r.Name).ToList();
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Take(12).ToArray();

            var data = new List<object>();
            int totalUsers = 0;
            int currentMonthUsers = 0;
            var currentMonth = DateTime.Today.Month;
            var lastMonth = currentMonth == 1 ? 12 : currentMonth - 1;

            foreach (var role in roles)
            {
                var counts = new List<int>();
                for (int i = 0; i < 12; i++)
                {
                    var count = __context.Users
                        .Count(x => x.Role.ToLower() == role.ToLower() && x.CreationDate.Month == i + 1);
                    counts.Add(count);
                    totalUsers += count;

                    if (i == currentMonth - 1)
                    {
                        currentMonthUsers += count;
                    }
                }

                data.Add(new { role = role, counts = counts });
            }

            var percentUser = currentMonthUsers == 0 ? 0 : (currentMonthUsers * 100 / totalUsers);
            var result = new
            {
                months = months,
                data = data,
                totalUsers = totalUsers,
                currentMonthUsers = currentMonthUsers,
                percentUser = percentUser
            };

            return Ok(result);
        }

        [HttpGet("UserLogonByDay")]
        public IActionResult GetUserLogonByDay()
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-6);

            var distinctUsersLogon = __context.LoginLogs
                .Where(l => l.LoginTime >= lastWeek)
                .Select(l => l.IdUser)
                .Distinct()
                .Count();

            var usersTotal = __context.Users.Count();

            var logonCounts = new List<int>();
            var dates = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                var logonDate = lastWeek.AddDays(i);

                var count = __context.LoginLogs
                    .Count(l => l.LoginTime.Date == logonDate.Date);

                logonCounts.Add(count);
                dates.Add(logonDate.ToShortDateString());
            }

            var result = new
            {
                dates = dates,
                logonCounts = logonCounts,
                recentLogonUsers = logonCounts.Sum(),
                percentUserLogonByTotal = usersTotal == 0 ? 0 : (int)Math.Round(((double)distinctUsersLogon / usersTotal) * 100)
            };

            return Ok(result);
        }



        [HttpGet("TransactionByClassAndType")]
        public IActionResult GetTransactionByClassAndType()
        {


            var today = DateTime.Today;
            var lastWeek = today.AddDays(-6);
            var classes = _context.TransactionClasses.ToList();
            var transactions = _context.Transactions.ToList();

            var transactionsByClass = classes.Select(c => new
      {
          TransactionClass = c.Name,
          TransactionClassDescription = c.Description,
          Transactions = _context.Transactions
                    .Where(t => t.TransactionClassNavigation!.Id == c.Id)
              .ToList()
            });

            var result = transactionsByClass.Select(tc => new
            {
                TransactionClass = tc.TransactionClass,
                TransactionClassDescription = tc.TransactionClassDescription,
                TypeQuantity = tc.Transactions.GroupBy(t => t.Type).Select(g => new
                {
                    Type = g.Key,
                    Quantity = g.Count(),
                    Balance = g.Sum(t => t.Type == EventTypeTransaction.Renda ? t.Value : -t.Value)
                }).ToList(),
                TotalQuantity = tc.Transactions.Count(),
                TotalBalance = tc.Transactions.Sum(t => t.Type == EventTypeTransaction.Renda ? t.Value : -t.Value),
                TypePercentages = tc.Transactions.GroupBy(t => t.TransactionClassNavigation.Name).Select(g => new
                {
                    Type = g.Key,
                    Percentage = ((double)g.Count() / tc.Transactions.Count()).ToString("P")
                }).ToList()
            }).ToList();

            return Ok(result);

            }).ToList();


            return Ok(result);
        }

        [HttpGet("TransactionByClassUser/{IdUser}")]
        public IActionResult GetTransactionByClassByUser(string idUser)
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-6);
            var userTransactions = _context.Transactions
                .Where(t => t.IdUser == idUser && t.Date >= lastWeek && t.Date <= today)
                .ToList();

            var result = new
            {
                TypeQuantity = userTransactions
                    .GroupBy(t => t.Type)
                    .Select(g => new
                    {
                        Type = g.Key,
                        Quantity = g.Count(),
                        Balance = g.Sum(t => t.Type == EventTypeTransaction.Renda ? t.Value : -t.Value)
                    }).ToList(),
                TotalQuantity = userTransactions.Count(),
                TotalBalance = userTransactions.Sum(t => t.Type == EventTypeTransaction.Renda ? t.Value : -t.Value),
                TypePercentages = userTransactions
                    .GroupBy(t => t.Type)
                    .Select(g => new
                    {
                        Type = g.Key,
                        Percentage = (g.Sum(t => t.Type == EventTypeTransaction.Renda ? t.Value : -t.Value) / userTransactions.Sum(t => t.Type == EventTypeTransaction.Renda ? t.Value : -t.Value)).ToString("P")
                    }).ToList()
            };

            return Ok(result);
        }



    }



}

