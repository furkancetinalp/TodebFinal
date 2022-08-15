using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFBase
{
    //Interface for databse of SQL server 
    public interface IEFBaseRepository<T> where T : class
    {
        //Generic methods which can be used for all repositories.
        void Add(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression=null);
        void Delete(T entity);
        T Get(Expression<Func<T,bool>> expression);
        void SaveChanges();
    }
}
