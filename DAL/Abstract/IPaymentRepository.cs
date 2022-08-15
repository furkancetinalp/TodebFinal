using DAL.MongoBase;
using Models.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    //Interface for Payments class of MongoDb
    public interface IPaymentRepository : IDocumentRepository<Payment>
    {
    }
}
