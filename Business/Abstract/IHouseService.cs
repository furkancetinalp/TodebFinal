using Bussines.Configuration.Response;
using DTO.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface definition and method decleration of Houses class
    public interface IHouseService
    {
        public IEnumerable<GetHouseRequest> GetAll();
        public CommandResponse Insert(CreateHouseRequest house);
        public CommandResponse Update(int houseNo, UpdateHouseRequest house);
        public CommandResponse Delete(int houseNo);
        public GetHouseRequest Get(int houseNo);

    }
}
