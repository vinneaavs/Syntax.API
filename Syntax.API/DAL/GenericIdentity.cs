using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;
using Syntax.Auth.Data;
using Syntax.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Syntax.API.DAL
{

    public enum OperationTypeIdentity
    {
        Detached = 0,
        Unchanged = 1,
        Deleted = 2,
        Modified = 3,
        Added = 4
    }
    public abstract class GenericIdentity<T> where T : class
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private IdentityContext _context { get; set; }
        public GenericIdentity(IdentityContext _context, UserManager<ApplicationUser> userManager)
        {
            this._context = _context;
            this._userManager = userManager;
            
        }

        public void Operation(T item, OperationType op)
        {
            if (op == OperationType.Modified)
            {
                var user = item as ApplicationUser;
                var originalUser = _context.Users.Find(user.Id);

                if (originalUser == null)
                {
                    throw new Exception("User not found");
                }
                if (originalUser.Role != user.Role)
                {
                    var roles = _userManager.GetRolesAsync(user).Result;
                    _userManager.RemoveFromRolesAsync(user, roles).Wait();
                    _userManager.AddToRoleAsync(user, user.Role).Wait();
                }

                _context.Entry(originalUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            else
            {
                _context.Entry<T>(item).State = (EntityState)op;
                _context.SaveChanges();
            }
           
        }
        public IEnumerable<T> List()
        
        {
            return _context.Set<T>().ToList();
        }
        public ApplicationUser? FindById(string id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
        public ApplicationUser? FindByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }
    }

}
