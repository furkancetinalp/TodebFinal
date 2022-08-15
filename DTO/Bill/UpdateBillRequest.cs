using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bill
{
    public class UpdateBillRequest
    {
        public int HouseNo { get; set; }
        public BillType Type { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
    }
}
