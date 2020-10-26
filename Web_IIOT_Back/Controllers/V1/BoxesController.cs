using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_IIOT_Back.Models;
using Web_IIOT_Back.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace Web_IIOT_Back.Controllers.V1
{
    //[EnableCors]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/V1/[controller]")]
    [ApiController]
    public class BoxesController : ControllerBase
    {
        private IBoxService boxService;

        public BoxesController(IBoxService boxService)
        {
            this.boxService = boxService ?? throw new ArgumentNullException(nameof(boxService));
        }

        // GET: api/V1/Box
        [HttpGet]
        public IEnumerable<Box> GetAllBoxes()
        {
            var boxes = boxService.GetAllBoxes();
            return boxes;
            //return JsonSerializer.Serialize(boxService.GetAllBoxes());
            //return new string[] { "value1", "value2" };
        }

        // GET: api/V1/Box/5
        [HttpGet("{id}")]//, Name = "Get")]
        public Box GetBox(int id)
        {
            return boxService.GetBox(id); //JsonSerializer.Serialize(boxService.GetBox(id));
        }

        // POST: api/V1/Box
        [HttpPost]
        public void Post([FromBody] Box box)
        {
            //New Box Can Be Added Only With New Equipment
            //return HttpStatusCode.Forbidden;
        }

        // PUT: api/V1/Box/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Box box)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
