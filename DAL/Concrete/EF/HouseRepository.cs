using DAL.Abstract;
using DAL.DbContexts;
using DAL.EFBase;
using DTO.House;
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
    public class HouseRepository:EFBaseRepository<House, ApartmentSystemDbContext>,IHouseRepository
    {
        public HouseRepository(ApartmentSystemDbContext context) : base(context)
        {

        }

        public IEnumerable<GetHouseRequest> GetAllHousesWithUserNames()
        {
            return null;
        }
    }
}
