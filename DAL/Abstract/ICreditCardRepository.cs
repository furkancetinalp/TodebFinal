using DAL.MongoBase;
using Models.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    //Interface definition of CreditCard class of MongoDb
    public interface ICreditCardRepository: IDocumentRepository<CreditCard>
    {
    }
}
