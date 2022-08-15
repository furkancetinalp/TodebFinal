using DAL.EFBase;
using DTO.House;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    //Interface definition of Houses class
    public interface IHouseRepository:IEFBaseRepository<House>
    {
        public IEnumerable<GetHouseRequest> GetAllHousesWithUserNames();
        
        
    }
}
