using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        //In case of verifying password
        [HttpGet("VerifyPassword")]
        public IActionResult VerifyPassword(string email,string password)
        {
            var response = _service.VerifyPassword(email, password);
            return Ok(response);
        }
        //Login process of users
        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            var response = _service.Login(email, password);
            return Ok(response);
        }
    }
}
