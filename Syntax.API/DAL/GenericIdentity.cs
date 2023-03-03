using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;
using Syntax.Auth.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private IdentityContext _context { get; set; }
        public GenericIdentity(IdentityContext _context)
        {
            this._context = _context;
        }

        public void Operation(T item, OperationType op)
        {
            _context.Entry<T>(item).State = (EntityState)op;
            _context.SaveChanges();
        }
        public IEnumerable<T> List()
        {
            return _context.Set<T>().ToList();
        }
        public T? FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }

}
