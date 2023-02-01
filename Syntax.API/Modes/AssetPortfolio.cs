using System.ComponentModel.DataAnnotations;

namespace Syntax.Models
{
    public class AssetPortfolio
    {
        [Key]public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int AssetId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

        public InvestmentPortfolio InvestmentPortfolio { get; set; }
        public Asset Asset { get; set; }

    }
}
