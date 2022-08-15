using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheTestController : ControllerBase
    {
        //Distributed Cache and InMemory Cache implementation
        private readonly ICacheExample _cacheExample;

        public CacheTestController(ICacheExample cacheExample)
        {
            _cacheExample = cacheExample;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _cacheExample.DistSetString("ApartmentSystemTestKey", "ApartmentSystemTestKeyValue");
            return Ok();
        }
        [HttpPost("SetList")]
        public IActionResult SetList()
        {
            _cacheExample.DistSetList("ApartmentSystemTestKeyList");
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = new List<string>();
            list.Add(_cacheExample.DistGetValue("ApartmentSystemTestKey"));
            list.Add(_cacheExample.DistGetValue("ApartmentSystemTestKeyList"));
            return Ok(list);
        }

        //IN MEMORY CACHE ***************************


        [HttpPost("InMemoryTest")]
        public IActionResult InMemoryTest()
        {
            _cacheExample.InMemSetString("InMemoryStr", "InMemoryStrExample");
            _cacheExample.InMemSetObject("InMemoryList", new int[] { 12, 21, 32, 63, 83, 75 });
            return Ok();
        }

        [HttpGet("GetInMemory")]
        public IActionResult GetInMemory()
        {
            var strValue = _cacheExample.InMemGetValue<string>("InMemoryStr");
            var listValue = _cacheExample.InMemGetValue<int[]>("InMemoryList");
            return Ok(new { strValue, listValue });
        }
    }
}
