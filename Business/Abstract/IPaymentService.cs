using Bussines.Configuration.Response;
using DTO.Bill;
using DTO.Payment;
using Models.Document;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface definition and method decleration of Payments class
    public interface IPaymentService
    {
        CommandResponse Add(int houseNo,BillType billType, int month,string CardNumber);
        GetPaymentRecordsRequest Get(int HouseNo);
        IEnumerable<GetPaymentRecordsRequest> GetAll();

    }
}
