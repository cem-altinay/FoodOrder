using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers
{
    [Authorize]
    public class SupplierController : BaseController
    {
        [HttpGet("supplierbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.Supplier.Queries.GetSupplierById { Id = id }, cancellationToken));
        }

        [HttpGet("allsuppliers")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.Supplier.Queries.GetSuppliers(), cancellationToken));
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(FoodOrder.Application.Features.Supplier.Commands.CreateSupplierCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(FoodOrder.Application.Features.Supplier.Commands.UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new  FoodOrder.Application.Features.Supplier.Commands.DeleteSupplierCommand(){Id=id }, cancellationToken));
        }
    }
}