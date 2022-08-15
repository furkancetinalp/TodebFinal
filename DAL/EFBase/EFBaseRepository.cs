using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFBase
{
    //Defining methods of interface for tables in SQL
    public class EFBaseRepository<T,TBContext> : IEFBaseRepository<T> 
        where TBContext : DbContext
        where T : class
    {
        protected readonly TBContext _context;
        public EFBaseRepository(TBContext context)
        {
            _context = context;
        }
    
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);

        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefault(expression);

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            if (expression is null)
                return _context.Set<T>().ToList(); 
            else
                return _context.Set<T>().Where(expression);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);

        }
    }
}
