using MediatR;
using MediatR_0.Command;
using MediatR_0.Entity;
using MediatR_0.Model;
using MediatR_0.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetCustomersQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery
            {
                Id = id
            });

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCustomerModel createCustomerModel)
        {
            //TODO: Use AutoMapper for mappings

            var customer = new Customer()
            {
                FirstName = createCustomerModel.FirstName,
                LastName = createCustomerModel.LastName,
                Birthday = createCustomerModel.Birthday,
                Age = createCustomerModel.Age,
                Phone = createCustomerModel.Phone,
            };

            var result = await _mediator.Send(new CreateCustomerCommand
            {
                Customer = customer
            });

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerModel updateCustomerModel)
        {
            //TODO: Use AutoMapper for mappings

            var existCustomer = await _mediator.Send(new GetCustomerByIdQuery
            {
                Id = updateCustomerModel.Id
            });

            if (existCustomer == null)
            {
                return BadRequest($"No customer found with the id {updateCustomerModel.Id}");
            }

            var customer = new Customer()
            {
                Id = updateCustomerModel.Id,
                FirstName = updateCustomerModel.FirstName,
                LastName = updateCustomerModel.LastName,
                Birthday = updateCustomerModel.Birthday,
                Age = updateCustomerModel.Age,
                Phone = updateCustomerModel.Phone,
            };

            var result = await _mediator.Send(new UpdateCustomerCommand
            {
                Customer = customer
            });

            return Ok(result);
        }
    }
}
