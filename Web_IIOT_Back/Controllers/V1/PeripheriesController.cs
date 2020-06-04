using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_IIOT_Back.Models;

namespace Web_IIOT_Back.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class PeripheriesController : ControllerBase
    {
        // GET: api/V1/Periphery
        [HttpGet]
        public IEnumerable<Periphery> GetPeripheries()
        {
            throw new ArgumentNullException();
            //return new string[] { "value1", "value2" };
        }

        // GET: api/V1/Periphery/5
        [HttpGet("{id}")]//, Name = "Get")]
        public string GetPeriphery(int id)
        {
            return "value";
        }

        // POST: api/V1/Periphery
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/V1/Periphery/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/V1/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
