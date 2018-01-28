using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Taobao.Area.Api.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
