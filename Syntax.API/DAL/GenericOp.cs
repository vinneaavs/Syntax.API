﻿using Microsoft.EntityFrameworkCore;
using Syntax.API.Context;
using Syntax.API.Models;

namespace Syntax.API.DAL
{
    public enum OperationType
    {
        Detached = 0,
        Unchanged = 1,
        Deleted = 2,
        Modified = 3,
        Added = 4
    }
    public abstract class GenericOp<T> where T : class
    {
        private ApplicationDbContext _context { get; set; }
        public GenericOp(ApplicationDbContext _context)
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
            return _context.Set<T>();
        }
        public T? FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }


        public bool FindByString(string name)
        {
            return _context.AssetsClasses.FirstOrDefault(x => x.Name == name) != null;
        }
    }
}
