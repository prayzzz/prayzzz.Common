using Microsoft.AspNetCore.Mvc;

namespace prayzzz.Common.Mvc.Results
{
    public class UnauthorizedObjectResult : ObjectResult
    {
        public UnauthorizedObjectResult(object value) : base(value)
        {
            StatusCode = 401;
        }
    }
}