using API.Configuration.Filters.Auth;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Document;
using Models.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController: ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)
        {
            _service = service;
        }
        //Method call of making payment for bills
        [HttpPost]
        public IActionResult MakePayment(int houseNo, BillType billType, int month, string CardNumber)
        {
            var response = _service.Add( houseNo,  billType,  month,  CardNumber);
            return Ok(response);
        }
        //Getting payment records by house number
        [HttpGet("GetByHouseNo")]
        [Permission(Permission.PaymentByHouseNo)]
        public IActionResult PaymentByHouseNo(int HouseNo)
        {
            var response = _service.Get(HouseNo);
            return Ok(response);
        }
        //Records of all payments
        [HttpGet("GetAll")]
        [Permission(Permission.AllPayments)]
        public IActionResult AllPayments()
        {
            var response = _service.GetAll();
            return Ok(response);
        }
    }
}
