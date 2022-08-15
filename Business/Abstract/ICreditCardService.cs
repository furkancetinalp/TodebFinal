using Bussines.Configuration.Response;
using DTO.CreditCard;
using Models.Document;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface definition and method decleration of Cards class
    public interface ICreditCardService
    {
        CommandResponse Add(CreateCreditCardRequest model);
        CommandResponse Update(string CardNumber,UpdateCreditCardRequest model);
        CommandResponse Delete(string CardNumber);
        GetCreditCardRequest Get(string CardNumber);

        void TestExceptionFilter();
    }
}
