using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax.Auth.Data
{
    [Table("LoginLog")]
    public class LoginLog
    {
        [Key]
        public int Id { get; set; }

        [Column("IdUser")]
        public string IdUser { get; set; }  
        
        [Column("UserName")]
        public string UserName { get; set; }

        [Column("LoginTime")]
        public DateTime LoginTime { get; set; }
    }
}
