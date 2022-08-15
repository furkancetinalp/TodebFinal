using DAL.Abstract;
using DAL.DbContexts;
using DAL.EFBase;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EF
{
    //Inheritance from related interface for Ef and  setting of methods
    public class MessageRepository : EFBaseRepository<Message, ApartmentSystemDbContext>, IMessageRepository
    {
        public MessageRepository(ApartmentSystemDbContext context) : base(context)
        {
        }
    }
}
