using System.ComponentModel.DataAnnotations;

namespace Syntax.API.Modes
{
    public class Logon
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set;}
        public bool? IsConfirmed { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool? IsLocked { get; set;}
    }
}
