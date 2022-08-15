using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.House
{
    public class GetHouseRequest
    {
        public int HouseNo { get; set; }
        public int FloorNo { get; set; }
        public string HouseBlock { get; set; }
        public bool IsHouseFilled { get; set; }
        public string Type { get; set; }
        public bool IsOwner { get; set; }
        public string ResidentName { get; set; }

    }
}
