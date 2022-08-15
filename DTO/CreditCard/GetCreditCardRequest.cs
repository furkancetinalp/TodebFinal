using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CreditCard
{
    public class GetCreditCardRequest
    {
        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }

        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
    }
}
