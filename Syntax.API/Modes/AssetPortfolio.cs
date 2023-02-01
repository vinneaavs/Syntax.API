using System.ComponentModel.DataAnnotations;

namespace Syntax.Models
{
    public class AssetPortfolio
    {
        [Key]public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public User User { get; set; }
        public Asset Asset { get; set; }

    }
}
