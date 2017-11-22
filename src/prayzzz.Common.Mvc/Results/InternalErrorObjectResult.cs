using Microsoft.AspNetCore.Mvc;

namespace prayzzz.Common.Mvc.Results
{
    public class InternalErrorObjectResult : ObjectResult
    {
        public InternalErrorObjectResult(object value) : base(value)
        {
            StatusCode = 500;
        }
    }
}