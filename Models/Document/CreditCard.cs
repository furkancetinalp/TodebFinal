using Models.Document.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Document
{
    //Credit card document for MongoDb
    public class CreditCard: DocumentBaseEntity
    {
        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
    }
}
