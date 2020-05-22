using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace prayzzz.Common.Mvc.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Get200")]
        public ActionResult<IEnumerable<string>> Get200()
        {
            return new StatusCodeResult(200);
        }
        
        [HttpGet("Get201")]
        public ActionResult<IEnumerable<string>> Get201()
        {
            return new StatusCodeResult(201);
        }

        // GET api/values
        [HttpGet("ex")]
        public ActionResult<IEnumerable<string>> Ex()
        {
            throw new Exception();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
