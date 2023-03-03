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

        public int IdApp { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }

}
