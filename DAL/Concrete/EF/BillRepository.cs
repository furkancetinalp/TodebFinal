using DAL.Abstract;
using DAL.DbContexts;
using DAL.EFBase;
using DTO.Bill;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.EF
{
    //Inheritance from related interface for Ef and setting of methods
    public class BillRepository : EFBaseRepository<Bill, ApartmentSystemDbContext>, IBillRepository
    {
        public BillRepository(ApartmentSystemDbContext context) : base(context)
        {
        }

        
    }
}
