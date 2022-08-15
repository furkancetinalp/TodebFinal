using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class House
    {
        [Key]
        public int Id { get; set; }
        public int HouseNo { get; set; }
        public int FloorNo { get; set; }
        public HouseBlock HouseBlock { get; set; }
        public bool IsHouseFilled { get; set; }
        public string Type { get; set; }
        public bool IsOwner { get; set; }
        public User User { get; set; }
    }
}
