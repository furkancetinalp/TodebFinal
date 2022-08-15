using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.House
{
    public class CreateHouseRequest
    {
        public int HouseNo { get; set; }
        public int FloorNo { get; set; }
        public HouseBlock HouseBlock { get; set; }
        public bool IsHouseFilled { get; set; }
        public string Type { get; set; }
        public bool IsOwner { get; set; }
    }
}
