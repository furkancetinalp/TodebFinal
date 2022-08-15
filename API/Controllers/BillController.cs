using API.Configuration.Filters.Auth;
using Business.Abstract;
using DTO.Bill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillController:ControllerBase
    {
        private readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        //Adding bill -- only by admin
        [HttpPost]
        [Permission(Permission.AddBill)]
        public IActionResult AddBill(CreateBillRequest bill)
        {
            var response = _billService.Insert(bill);
            return Ok(response);
        }
        
        //Getting exact bill -- users + admin
        [HttpGet("GetSpecificBill")]
        public IActionResult SpecificBill(int houseNo, BillType billType, int month)
        {
            var response = _billService.GetSpecificBill(houseNo, billType, month);
            return Ok(response);
        }
        //Getting all bills -- only by admin
        [HttpGet]
        [Permission(Permission.GetAllBills)]
        public IActionResult AllBills()
        {
            var result = _billService.GetAll();
            return Ok(result);
        }

        //Assigning monthly fee bills of houses all together --only by admin
        [HttpPost("AssignFeeBills")]
        [Permission(Permission.AssignFeeBills)]
        public IActionResult AssignFeeBills(decimal totalFee, int month)
        {
            var response = _billService.AssignFeeInBulk(totalFee, month);
            return Ok(response);
        }

        //Assigning monthly electricity bills of houses all together --only by admin
        [HttpPost("AssignElectricityBills")]
        [Permission(Permission.AssignElectricityBills)]
        public IActionResult AssignElectricityBills(decimal totalFee, int month)
        {
            var response = _billService.AssignElectricityInBulk(totalFee, month);
            return Ok(response);
        }

        //Assigning monthly water bills of houses all together --only by admin
        [HttpPost("AssignWaterBills")]
        [Permission(Permission.AssignWaterBills)]
        public IActionResult AssignWaterBills(decimal totalFee, int month)
        {
            var response = _billService.AssignWaterInBulk(totalFee, month);
            return Ok(response);
        }

        //Assigning monthly gas bills of houses all together --only by admin
        [HttpPost("AssignGasBills")]
        [Permission(Permission.AssignGasBills)]
        public IActionResult AssignGasBills(decimal totalFee, int month)
        {
            var response = _billService.AssignGasInBulk(totalFee, month);
            return Ok(response);
        }

        //Getting bills by bill type --only by admin
        [HttpGet("BillType")]
        [Permission(Permission.GetByBillType)]
        public IActionResult ByBillType(BillType billtype)
        {
            var response = _billService.GetBillsByBillType(billtype);
            return Ok(response);
        }
        //Getting all bills for a specific house -- admin + users
        [HttpGet("GetBillsByHouseNumber")]
        public IActionResult ByHouseNumber(int houseNo)
        {
            var response = _billService.GetBillsByHouse(houseNo);
            return Ok(response);
        }

        //Getting  specific bills by given month -- admin + users
        [HttpGet("GetByBillTypeAndMonth")]
        public IActionResult ByBTypeAndMonth(BillType billType, int month)
        {
            var response = _billService.GetByBillTypeAndMonth( billType,  month);
            return Ok(response);
        }   

        //Amount of monthly debt by bill type -- only by admin
        [HttpGet("TotalAmountOfDebtByMonthAndBill")]
        [Permission(Permission.GetTotalAmountOfDebtByMonthAndBill)]
        public IActionResult TotalAmountOfDebtByMonthAndBill(int month, BillType billType)
        {
            var response = _billService.MonthlyDebtListByBillType(month, billType);
            return Ok(response);
        }

        //Deleting bill -- only by admin
        [HttpDelete]
        [Permission(Permission.DeleteBill)]
        public IActionResult DeleteBill(int houseNo, BillType billType, int month)
        {
            var result = _billService.Delete(houseNo, billType, month);
            return Ok(result);
        }

    }
}
