using Microsoft.EntityFrameworkCore;

namespace Syntax.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetClass> AssetsClass { get; set; }
    public DbSet<AssetPortfolio> Portfolios { get; set; }
    public DbSet<InvestmentPortfolio> Investments { get; set; }
    public DbSet<User> Users { get; set; }

}