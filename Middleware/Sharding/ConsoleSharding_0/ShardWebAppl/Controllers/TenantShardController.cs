using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Command.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShardWebAppl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantShardController : ApiBaseController
    {
        public TenantShardController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateShardCommand command, CancellationToken cancellationToken)
        {
            //using (var operation = _telemetryClient.StartOperation<RequestTelemetry>("CreateTenantShard"))
            {
                var result = await mediator.Send(command, cancellationToken);
                return Ok(result);
            }
        }
    }
}
