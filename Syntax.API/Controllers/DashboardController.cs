using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
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

            decimal totalExpenses = transactions.Sum(t => t.Type == EventTypeTransaction.Despesas ? -t.Value : 0);
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
                    Percentage = (totalExpenses == 0) ? 0 : ((tc.Transactions.Sum(t => t.Type == EventTypeTransaction.Despesas ? -t.Value : 0) * -1) / (totalExpenses * -1) * 100)
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        /* ------------------------------------------------------------------------------------------------------------------ */

        [HttpGet("UserBalanceCurrentMonth/{idUser}")]
        public IActionResult GetUserBalanceCurrentMonth(string idUser)
        {
            var today = DateTime.Today;
            var transactions = _context.Transactions
                .Include(t => t.TransactionClassNavigation)
                .Where(t => t.Date.Year == today.Year && t.Date.Month == today.Month && t.IdUser == idUser)
                .ToList();

            decimal balance = transactions.Sum(t => t.Type == EventTypeTransaction.Renda ? t.Value : -t.Value);

            return Ok(balance);
        }

        [HttpGet("UserNetWorth/{idUser}")]
        public IActionResult GetUserNetWorth(string idUser)
        {
            var assets = _context.AssetPortfolios
                .Include(ap => ap.PortFolioNavigation)
                .Where(ap => ap.PortFolioNavigation!.IdUser == idUser)
                .ToList();

            decimal totalPurchasePrice = assets.Sum(ap => ap.PurchasePrice);


            return Ok(totalPurchasePrice);
        }

        /* ------------------------------------------------------------------------------------------------------------------ */

        [HttpGet("UserBalanceLastThreeMonths/{idUser}")]
        public IActionResult GetUserBalanceLastThreeMonths(string idUser)
        {
            var today = DateTime.Today;
            var threeMonthsAgo = today.AddMonths(-3);

            var transactions = _context.Transactions
                .Include(t => t.TransactionClassNavigation)
                .Where(t => t.Date >= threeMonthsAgo && t.Date <= today && t.IdUser == idUser)
                .ToList();

            var groupedTransactions = transactions
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Expenses = g.Where(t => t.Type == EventTypeTransaction.Despesas).Sum(t => t.Value),
                    Revenue = g.Where(t => t.Type == EventTypeTransaction.Renda).Sum(t => t.Value)
                })
                .OrderBy(t => t.Month)
                .ToList();

            var balances = groupedTransactions
                .Select(t => new
                {
                    Month = t.Month.ToString("MMMM"),
                    Expenses = t.Expenses,
                    Revenue = t.Revenue
                })
                .ToList();

            return Ok(balances);
        }

        [HttpGet("ExpensePercentageByClassUser/{idUser}")]
        public IActionResult GetExpensePercentageByClassByUser(string idUser)
        {
            var classes = _context.TransactionClasses.ToList();

            var transactionsByClass = classes
                .Where(c => _context.Transactions
                    .Any(t => t.TransactionClassNavigation!.Id == c.Id && t.IdUser == idUser && t.Type == EventTypeTransaction.Despesas))
                .Select(c => new
                {
                    TransactionClass = c.Name,
                    Transactions = _context.Transactions
                        .Where(t => t.TransactionClassNavigation!.Id == c.Id && t.IdUser == idUser && t.Type == EventTypeTransaction.Despesas)
                        .ToList()
                });

            decimal totalIncome = transactionsByClass.Sum(tc => tc.Transactions.Sum(t => t.Value));
            var result = transactionsByClass.Select(tc => new
            {
                TransactionClass = tc.TransactionClass,
                ClassBalance = tc.Transactions.Sum(t => t.Value),
                ClassPercentage = (totalIncome == 0) ? 0 : ((tc.Transactions.Sum(t => t.Value) * 1) / totalIncome * 100)
            }).ToList();

            return Ok(result);
        }

        [HttpGet("IncomePercentageByClassUser/{idUser}")]
        public IActionResult GetIncomePercentageByClassByUser(string idUser)
        {
            var classes = _context.TransactionClasses.ToList();

            var transactionsByClass = classes
                .Where(c => _context.Transactions
                    .Any(t => t.TransactionClassNavigation!.Id == c.Id && t.IdUser == idUser && t.Type == EventTypeTransaction.Renda))
                .Select(c => new
                {
                    TransactionClass = c.Name,
                    Transactions = _context.Transactions
                        .Where(t => t.TransactionClassNavigation!.Id == c.Id && t.IdUser == idUser && t.Type == EventTypeTransaction.Renda)
                        .ToList()
                });

            decimal totalIncome = transactionsByClass.Sum(tc => tc.Transactions.Sum(t => t.Value));
            var result = transactionsByClass.Select(tc => new
            {
                TransactionClass = tc.TransactionClass,
                ClassBalance = tc.Transactions.Sum(t => t.Value),
                ClassPercentage = (totalIncome == 0) ? 0 : ((tc.Transactions.Sum(t => t.Value) * 1) / totalIncome * 100)
            }).ToList();

            return Ok(result);
        }


        [HttpGet("PortfolioEvolutionByUser/{idUser}")]
        public IActionResult GetPortfolioEvolutionByUser(string idUser)
        {
            var currentDate = DateTime.Now;
            var result = new List<object>();
            decimal accumulatedPurchasePrice = 0;

            var assetPortfolios = _context.AssetPortfolios
                .Include(ap => ap.PortFolioNavigation)
                .Include(ap => ap.AssetNavigation)
                .ThenInclude(a => a!.AssetClassNavigation)
                .Where(ap => ap.PortFolioNavigation!.IdUser == idUser)
                .AsEnumerable() // adicionado para forçar a execução no lado do cliente
                .GroupBy(ap => ap.Date.Day)
                .OrderBy(g => g.Key)
                .ToList();

            foreach (var assetPortfolio in assetPortfolios)
            {
                decimal purchasePrice = assetPortfolio.Sum(ap => ap.PurchasePrice);
                accumulatedPurchasePrice += purchasePrice;

                result.Add(new
                {
                    day = assetPortfolio.Key,
                    purchasePrice = accumulatedPurchasePrice
                });
            }

            return Ok(result);
        }

        [HttpGet("AssetByClassByUser/{idUser}")]
        public IActionResult GetAssetByClassByUser(string idUser)
        {
            var assetPortfolios = _context.AssetPortfolios
                .Include(ap => ap.PortFolioNavigation)
                .Include(ap => ap.AssetNavigation)
                    .ThenInclude(a => a!.AssetClassNavigation)
                .Where(ap => ap.PortFolioNavigation!.IdUser == idUser)
                .ToList();

            var result = assetPortfolios
                .GroupBy(ap => ap.AssetNavigation!.IdAssetClass)
                .Select(g => new
                {
                    AssetClassName = g.First().AssetNavigation!.AssetClassNavigation!.Name,
                    Amount = g.Sum(ap => ap.PurchasePrice)
                });

            return Ok(result);
        }

        [HttpGet("AssetByPortfolioByUser/{idUser}")]
        public IActionResult GetAssetByPortfolioByUser(string idUser)
        {
            var assetPortfolios = _context.AssetPortfolios
                .Include(ap => ap.PortFolioNavigation)
                .Include(ap => ap.AssetNavigation)
                .ThenInclude(a => a!.AssetClassNavigation)
                .Where(ap => ap.PortFolioNavigation!.IdUser == idUser)
                .ToList();

            var result = assetPortfolios
                .GroupBy(ap => new { ap.IdPortfolio })
                .Select(g => new
                {
                    PortfolioName = g.First().PortFolioNavigation!.Name,
                    Amount = g.Sum(ap => ap.PurchasePrice)
                });

            return Ok(result);
        }


    }
}

