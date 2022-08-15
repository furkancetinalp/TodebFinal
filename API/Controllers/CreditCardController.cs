using API.Configuration.Filters.Auth;
using API.Configuration.Filters.Log;
using Business.Abstract;
using DTO.CreditCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Document;
using Models.Entities;
using MongoDB.Bson;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService _service;

        public CreditCardController(ICreditCardService service)
        {
            _service = service;
        }
        /*
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var data = _service.GetAll();
            return Ok(data);
        }
        */
        //Getting credit card info
        [HttpGet("GetByCardNumber")]
        public IActionResult GetByCardNumber(string cardNumber)
        {

            var response = _service.Get(cardNumber);
            return Ok(response);
        }

        //Adding credit card by user to MongoDb
        [HttpPost]
        //[LogFilter]
        public IActionResult Post(CreateCreditCardRequest request)
        {
            var response=_service.Add(request);
            return Ok(response);
        }
        //Updating credit card
        [HttpPut("ByCardNumber")]
        public IActionResult ByCardNumber(string CardNumber,UpdateCreditCardRequest request)
        {
            var response=_service.Update(CardNumber,request);
            return Ok(response);
        }
        //Deleting credit card
        [HttpDelete]
        public IActionResult Delete(string cardNumber)
        {
            var response=_service.Delete(cardNumber);
            return Ok(response);
        }
        //Common exception message format for entire program
        [HttpGet("TestExceptionFilter")]
        public IActionResult TestExceptionFilter()
        {
            _service.TestExceptionFilter();
            return Ok();
        }
    }
}
