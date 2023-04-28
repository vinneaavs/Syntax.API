using Microsoft.EntityFrameworkCore;
using Syntax.API.Models;

namespace Syntax.API.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }


    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionClass> TransactionClasses { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<AssetClass> AssetsClasses { get; set; }
    public DbSet<AssetPortfolio> AssetPortfolios { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public ApplicationDbContext Context { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>()
            .HasOne(a => a.AssetClassNavigation)
            .WithMany()
            .HasForeignKey(a => a.IdAssetClass);

        //modelBuilder.Entity<Asset>()
        //    .Ignore(a => a.AssetClassNavigation);



        modelBuilder.Entity<AssetPortfolio>()
            .HasOne(a => a.PortFolioNavigation)
            .WithMany()
            .HasForeignKey(a => a.IdPortfolio);

        //modelBuilder.Entity<AssetPortfolio>()
        //    .Ignore(a => a.PortFolioNavigation);


        modelBuilder.Entity<AssetPortfolio>()
            .HasOne(a => a.AssetNavigation)
            .WithMany()
            .HasForeignKey(a => a.IdAsset);

        //modelBuilder.Entity<AssetPortfolio>()
        //    .Ignore(a => a.AssetNavigation);




        modelBuilder.Entity<Portfolio>()
           .HasOne(a => a.UserNavigation)
           .WithMany()
           .HasForeignKey(a => a.IdUser);

        //modelBuilder.Entity<Portfolio>()
        //    .Ignore(a => a.UserNavigation);



        modelBuilder.Entity<Transaction>()
         .HasOne(a => a.UserNavigation)
         .WithMany()
         .HasForeignKey(a => a.IdUser);

        //modelBuilder.Entity<Transaction>()
        //    .Ignore(a => a.UserNavigation);

        modelBuilder.Entity<Transaction>()
        .HasOne(a => a.TransactionClassNavigation)
        .WithMany()
        .HasForeignKey(a => a.IdTransactionClass);

        //modelBuilder.Entity<Transaction>()
        //    .Ignore(a => a.TransactionClassNavigation);

    }
}