using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityNo { get; set; }
        public int HouseNo { get; set; }
        [ForeignKey("HouseNo")]
        public House House { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string CarInfo { get; set; }
        public UserPassword UserPassword { get; set; }
        public UserRole UserRole { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<UserPermission> Permissions { get; set; }


    }
}
