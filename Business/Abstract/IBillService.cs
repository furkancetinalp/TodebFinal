using Bussines.Configuration.Response;
using DTO.Bill;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //Interface definition and method decleration of Bills class
    public interface IBillService
    {
        public IEnumerable<GetBillRequest> GetAll();
        public CommandResponse Insert(CreateBillRequest bill);
        public CommandResponse Delete(int houseNo, BillType billType, int month);
        public IEnumerable<GetBillRequest> GetBillsByHouse(int HouseNo);
        public IEnumerable<GetBillRequest> GetBillsByBillType(BillType billType);

        public GetBillRequest GetSpecificBill(int houseNo, BillType billType, int month);

        public CommandResponse AssignFeeInBulk(decimal totalFee, int month);
        public CommandResponse AssignElectricityInBulk(decimal totalFee, int month);

        public CommandResponse AssignWaterInBulk(decimal totalFee, int month);
        public CommandResponse AssignGasInBulk(decimal totalFee, int month);


        public IEnumerable<GetBillRequest> GetByBillTypeAndMonth(BillType billType, int month);

        public CommandResponse MonthlyDebtListByBillType(int month, BillType billType);
        

    }
}
