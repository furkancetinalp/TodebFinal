using API.Configuration.Filters.Auth;
using Business.Abstract;
using DTO.House;
using DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        //At initial, an admin should be created.This is the first step of application 
        [HttpPost("CreateAdmin")]
        [AllowAnonymous]
        public IActionResult CreateAdmin(CreateAdminRequest register)
        {
            var response = _service.CreateAdmin(register);
            return Ok(response);
        }
        //Adding user to database by admin
        [HttpPost("AddUser")]
        [Permission(Permission.Register)]
        public IActionResult AddUser(CreateUserRegisterRequest register)
        {
            var response =_service.Register(register);
            return Ok(response);
        }
        //Getting list of the users
        [HttpGet]
        [Permission(Permission.GetAllUsers)]
        public IActionResult AllUsers()
        {
            var response= _service.GetAll();
            return Ok(response);
        }
        //User by identity number
        [HttpGet("GetByIdentityNumber")]
        public IActionResult UserByIdentityNumber(string identityNumber)
        {
            var response = _service.GetByIdentityNumber(identityNumber);
            return Ok(response);
        }
        //Updating the user
        [HttpPut]
        [Permission(Permission.UpdateUser)]
        public IActionResult UpdateUser(string identityNumber, UpdateUserRequest user)
        {
            var response = _service.Update(identityNumber, user);
            return Ok(response);
        }
        //Deleting the user
        [HttpDelete]
        [Permission(Permission.DeleteUser)]
        public IActionResult DeleteUser(string identityNumber)
        {
            var response= _service.Delete(identityNumber);
            return Ok(response);
        }
        //User by hoyse number
        [HttpGet("GetByHouseNumber")]
        public IActionResult UserByHouseId(int houseNumber)
        {
            var response = _service.GetUserByHouseNo(houseNumber);
            return Ok(response);
        }
    }
}
