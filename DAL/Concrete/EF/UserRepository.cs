using DAL.Abstract;
using DAL.DbContexts;
using DAL.EFBase;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EF
{
    //Inheritance from related interface for Ef and  setting of methods
    public class UserRepository : EFBaseRepository<User, ApartmentSystemDbContext>, IUserRepository
    {
        public UserRepository(ApartmentSystemDbContext context) : base(context)
        {
        }

        //Setting an extra method
        public User GetUserWithPassword(string email)
        {
            return _context.Users.Include(x => x.UserPassword).FirstOrDefault(x => x.Mail == email);
        }
    }
}
