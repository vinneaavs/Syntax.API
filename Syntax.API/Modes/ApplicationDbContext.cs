using Microsoft.EntityFrameworkCore;

namespace Syntax.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }
    

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionClass> TransactionClasses { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetClass> AssetsClasses { get; set; }
    public DbSet<AssetPortfolio> AssetPortfolios { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<User> Users { get; set; }

}