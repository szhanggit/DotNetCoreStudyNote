using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShardWebAppl.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TXC.Common.Data.Core;
using TXC.Common.Data.TenantDbConnection;
using TXC.Common.Domain;

namespace ShardWebAppl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TxProductController : ControllerBase
    {
        private IDbConnection _dbConnection;
        private readonly ITenantDbConnection _tenantDbConnection;
        private readonly IDapperOperation _dapperOperation;

        public TxProductController(ITenantDbConnection tenantDbConnection, IDapperOperation dapperOperation)
        {
            _tenantDbConnection = tenantDbConnection;
            _dapperOperation = dapperOperation;
        }

        [HttpGet]
        [HttpGet("GetProductBrand")]
        public async Task<IActionResult> Get([FromQuery] GetProductBrandQuery query, CancellationToken cancellationToken)
        {
            int TenantId = 7;
            var conn = await _tenantDbConnection.GetTenantDbConnection(TenantId.ToString(), false, default);
            BrandDto _brandDto = new BrandDto
            {
                BrandId = 1,
                BrandName = "Ali",
                BrandStatus = 0
            };
            Response<BrandDto> _response = new Response<BrandDto> { Success = true, Message = "success", Data = _brandDto };
            return Ok(_response);
        }
    }
}
