using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.API.Models
{
    public class Asset
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Description { get; set; }
        #region ALTERAÇÃO DE ABORGAGEM
        //public AssetClass AssetClass { get; set; }
        #endregion
        public int IdAssetClass { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public int Grade { get; set; }
        //[ForeignKey(nameof(IdAssetClass))]
        public virtual AssetClass? AssetClassNavigation { get; set; }

    }
}
