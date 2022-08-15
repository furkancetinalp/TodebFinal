using DAL.Abstract;
using DAL.MongoBase;
using Microsoft.Extensions.Configuration;
using Models.Document;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.Mongo
{
    public class PaymentRepository: DocumentRepository<Payment>, IPaymentRepository
    {
        //Table name of Payment records from MongoDb

        private const string CollectionName = "Payments";
        public PaymentRepository(MongoClient client, IConfiguration configuration) : base(client, configuration, CollectionName)
        {
        }

    }
}
