using DAL.EFBase;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    //Interface definition of Users class
    public interface IUserRepository: IEFBaseRepository<User>
    {
        public User GetUserWithPassword(string email);
        
    }
}
