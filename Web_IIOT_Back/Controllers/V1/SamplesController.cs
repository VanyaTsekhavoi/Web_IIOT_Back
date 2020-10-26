using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers_Layer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_IIOT_Back.Services;

namespace Web_IIOT_Back.Controllers.V1
{
    //[EnableCors]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/V1/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {

        private IInitControllersLayerService initControllersLayerService;

        public SamplesController(IInitControllersLayerService initControllersLayerService)
        {
            this.initControllersLayerService = initControllersLayerService ?? throw new ArgumentNullException(nameof(initControllersLayerService));
        }

        // GET: api/V1/Samples
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { Logic.status }; //new string[] { "ok" };
        }

        // GET: api/V1/Samples/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/V1/Samples
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/V1/Samples/5
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
