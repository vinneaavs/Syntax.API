﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;
using Syntax.API.DAL;
using Syntax.API.Models;
using Syntax.Application.DTOs.Request;
using Syntax.Application.DTOs.Response;
using Syntax.Application.Interfaces.Services;
using Syntax.Auth.Data;
using System.Security.Claims;

namespace Syntax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator")]

    public class UserController : ControllerBase
    {
        private IIdentityService _identityService;
        private readonly UserDao _userDao;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserController(IIdentityService identityService, IdentityContext _context, UserManager<ApplicationUser> userManager)
        {
            _identityService = identityService;
            _userManager = userManager;
            _userDao = new UserDao(_context, userManager);

        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest userRegisterRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _identityService.RegisterUser(userRegisterRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            else if (result.Errors.Count > 0)
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest userLoginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _identityService.Login(userLoginRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }


        }
        [HttpGet]
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
      
            return _userDao.List().ToList();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ApplicationUser GetUserById(string id)
        {
            var user = _userDao.FindById(id);
            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public ApplicationUser CreateUser(ApplicationUser user)
        {
            _userDao.OperationAsync(user, OperationType.Added);
            return user;
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public ApplicationUser EditUser(ApplicationUser user)
        {
            _userDao.OperationAsync(user, OperationType.Modified);
            return user;
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        public IActionResult DeleteUser(ApplicationUser user)
        {
            try
            {
                _userDao.OperationAsync(user, OperationType.Deleted);
                return Ok($"Usuario {user.UserName} - {user.Id} Deletado com Sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteUserById(string id)
        {
            try
            {
                var user = _userDao.FindById(id);

                _userDao.OperationAsync(user!, OperationType.Deleted);
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}