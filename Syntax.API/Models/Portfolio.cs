using Syntax.Auth.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.API.Models
{
    public class Portfolio
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        #region ALTERAÇÃO DE ABORDAGEM
        //public User? User { get; set; }
        #endregion
        public DateTime? CreationDate { get; set; } = DateTime.Now;

        public string IdUser { get; set; }
        [ForeignKey(nameof(IdUser))]

        public virtual ApplicationUser UserNavigation { get; set; }

    }
}
