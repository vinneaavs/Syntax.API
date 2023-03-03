using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax.Application.DTOs.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "O Campo {0} é Obrigatório !")]
        [EmailAddress(ErrorMessage = "O Campo {0} é Inválido !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo {0} é Obrigatório !")]
        public string Password { get; set; }
    }
}
