using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace prayzzz.Common.Mvc.Sample.Areas.Products.Controllers
{
    [ApiController]
    [Route("api/products/detail")]
    public class DetailController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", "value2" };
        }
    }
}
