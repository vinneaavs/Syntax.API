using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syntax.API.Migrations
{
    /// <inheritdoc />
    public partial class newmigtest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IdTransactionClass",
                table: "Transactions",
                column: "IdTransactionClass");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_IdAssetClass",
                table: "Assets",
                column: "IdAssetClass");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPortfolios_IdAsset",
                table: "AssetPortfolios",
                column: "IdAsset");

            migrationBuilder.CreateIndex(
                name: "IX_AssetPortfolios_IdPortfolio",
                table: "AssetPortfolios",
                column: "IdPortfolio");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetPortfolios_Assets_IdAsset",
                table: "AssetPortfolios",
                column: "IdAsset",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetPortfolios_Portfolios_IdPortfolio",
                table: "AssetPortfolios",
                column: "IdPortfolio",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetsClasses_IdAssetClass",
                table: "Assets",
                column: "IdAssetClass",
                principalTable: "AssetsClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionClasses_IdTransactionClass",
                table: "Transactions",
                column: "IdTransactionClass",
                principalTable: "TransactionClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetPortfolios_Assets_IdAsset",
                table: "AssetPortfolios");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetPortfolios_Portfolios_IdPortfolio",
                table: "AssetPortfolios");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetsClasses_IdAssetClass",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionClasses_IdTransactionClass",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_IdTransactionClass",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Assets_IdAssetClass",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_AssetPortfolios_IdAsset",
                table: "AssetPortfolios");

            migrationBuilder.DropIndex(
                name: "IX_AssetPortfolios_IdPortfolio",
                table: "AssetPortfolios");
        }
    }
}
