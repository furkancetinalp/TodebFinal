using Models.Document.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Document
{
    //Payment document for MongoDb
    public class Payment:DocumentBaseEntity
    {
        public int HouseNo { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
