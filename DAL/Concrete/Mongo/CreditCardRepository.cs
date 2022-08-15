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
    public class CreditCardRepository: DocumentRepository<CreditCard>, ICreditCardRepository
    {
        //Table name of credit card from MongoDb
        private const string CollectionName = "CreditCard";

        public CreditCardRepository(MongoClient client, IConfiguration configuration) : base(client, configuration, CollectionName)
        {
        }
    }
}
