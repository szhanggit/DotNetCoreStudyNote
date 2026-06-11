using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TXCFluValidation.Models;

namespace TXCFluValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiBaseController
    {
        public ProductController() : base()
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto request, CancellationToken cancellationToken)
        {
            bool _success = true;
            if (ModelState.IsValid)
            {                
                if (_success)
                    return Ok(1);
                else
                    return BadRequest(0);
            }

            return BadRequest(FlattenModelStateErrorMessage());
        }
    }
}
