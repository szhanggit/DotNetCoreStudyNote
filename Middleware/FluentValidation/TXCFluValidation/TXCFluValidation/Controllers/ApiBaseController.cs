using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TXCFluValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {

        public ApiBaseController()
        {

        }

        internal string FlattenModelStateErrorMessage()
        {
            return string.Join(" | ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage));
        }
    }
}
