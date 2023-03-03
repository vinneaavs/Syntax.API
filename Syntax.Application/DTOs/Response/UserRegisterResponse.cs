using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax.Application.DTOs.Response
{
    public class UserRegisterResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public UserRegisterResponse() =>
            Errors = new List<string>();
        public UserRegisterResponse(bool success = true) : this() =>
            Success = success;

        public void AddErrors(IEnumerable<string> errors) =>
            Errors.AddRange(errors);

    }
}
