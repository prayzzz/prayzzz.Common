using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace prayzzz.Common.Mvc.Sample.Areas.Products.Controllers
{
    [Area("Products")]
    public class StockController : Controller
    {
        [HttpGet]
        public ViewResult Index(string id)
        {
            return View();
        }
    }
}
