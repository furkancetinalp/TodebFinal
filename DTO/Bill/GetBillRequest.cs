using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Bill
{
    public class GetBillRequest
    {
        public int HouseNo { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public DateTime DueDate { get; set; }
        public string Owner { get; set; }
        public bool IsPaymentMade { get; set; }= false;

    }
}
