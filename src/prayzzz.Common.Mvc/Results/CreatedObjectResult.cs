using Microsoft.AspNetCore.Mvc;

namespace prayzzz.Common.Mvc.Results
{
    public class CreatedObjectResult : ObjectResult
    {
        public CreatedObjectResult(object value) : base(value)
        {
            StatusCode = 201;
        }
    }
}