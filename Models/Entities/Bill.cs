using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public int HouseNo { get; set; }
        [ForeignKey("HouseNo")]
        public House House { get; set; }
        public BillType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public bool IsPaymentMade { get; set; }

    }
}
