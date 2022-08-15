using API.Configuration.Filters.Auth;
using Business.Abstract;
using DTO.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _service;
        public HouseController(IHouseService service)
        {
            _service = service;
        }
        //Adding house -- only by admin
        [HttpPost]
        [Permission(Permission.AddHouse)]
        public IActionResult AddHouse(CreateHouseRequest request)
        {
            var result = _service.Insert(request);
            return Ok(result);
        }
        //List of houses -- only by admin
        [HttpGet]
        [Permission(Permission.GetAllHouses)]
        public IActionResult AllHouses()
        {
            var data = _service.GetAll();
            return Ok(data);
        }
        //Getting house by house number -- only by admin
        [HttpGet("{houseNo}")]
        [Permission(Permission.GetByHouseNo)]
        public IActionResult ByHouseNo(int houseNo)
        {
            var data = _service.Get(houseNo);
            return Ok(data);
        }

        //Updating house -- only by admin
        [HttpPut("{houseNo}")]
        [Permission(Permission.UpdateHouse)]
        public IActionResult UpdateHouse(int houseNo, UpdateHouseRequest request)
        {
            var result = _service.Update(houseNo, request);
            return Ok(result);
        }
        //Deleting house -- only by admin
        [HttpDelete("{houseNo}")]
        [Permission(Permission.DeleteHouse)]
        public IActionResult DeleteHouse(int houseNo)
        {
            var result = _service.Delete(houseNo);
            return Ok(result);
        }

    }
}
