using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Syntax.API.Modes
{
    public class UserAuth
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        
        [Compare("Password")]
        public string? ReEntryPassword { get; set; }
        public string? Role { get; set; }
    }
}
