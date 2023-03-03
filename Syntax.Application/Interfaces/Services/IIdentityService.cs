using Syntax.Application.DTOs.Request;
using Syntax.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterRequest userRegisterRequest);
        Task<UserLoginResponse> Login(UserLoginRequest userLoginRequest);
    }

}
