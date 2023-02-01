using System.ComponentModel.DataAnnotations;

namespace Syntax.Models
{
    public class InvestmentPortfolio
    {
        [Key]public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public User? User { get; set; }
        public List<AssetPortfolio> AssetPortfolio { get; set; }
    }
}
