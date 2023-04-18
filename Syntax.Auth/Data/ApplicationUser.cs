using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax.Auth.Data
{


    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        public string? LastName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Role { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string FullName => $"{LastName}, {Name}";


    }

}
