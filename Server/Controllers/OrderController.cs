using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        #region  Order Operations
        [HttpGet("orderbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.Order.Queries.GetOrderById { Id = id }, cancellationToken));
        }

        [HttpGet("todayorders")]
        public async Task<IActionResult> GetTodayOrders(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.Order.Queries.GetOrders() { OrderDate = DateTime.UtcNow }, cancellationToken));
        }

        [HttpGet("ordersbydate/{orderDate}")]
        public async Task<IActionResult> GetOrdersByDate(DateTime? orderDate, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.Order.Queries.GetOrders() { OrderDate = orderDate }, cancellationToken));
        }

        [HttpGet("ordersbyfilter")]
        public async Task<IActionResult> GetOrdersByFilter(FoodOrder.Application.Features.Order.Queries.GetOrderByFilter request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create(FoodOrder.Application.Features.Order.Commands.CreateOrderCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(FoodOrder.Application.Features.Order.Commands.UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(FoodOrder.Application.Features.Order.Commands.DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
        #endregion

        #region  order Item Operations
        [HttpGet("orderitembyid/{id}")]
        public async Task<IActionResult> GetOrderItemById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.OrderItem.Queries.GetOrderItemById { Id = id }, cancellationToken));
        }

        [HttpGet("orderitemsbyorderid/{orderid}")]
        public async Task<IActionResult> GetOrderItemByOrderId(Guid orderid, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.OrderItem.Queries.GetOrderItems { orderId = orderid }, cancellationToken));
        }

        [HttpPost("createorderitem")]
        public async Task<IActionResult> CreateOrderItem(FoodOrder.Application.Features.OrderItem.Commands.CreateOrderItemCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("updateorderitem")]
        public async Task<IActionResult> UpdateOrderItem(FoodOrder.Application.Features.OrderItem.Commands.UpdateOrderItemCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("deleteorderitem")]
        public async Task<IActionResult> DeleteOrderItem(FoodOrder.Application.Features.OrderItem.Commands.DeleteOrderItemCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
        #endregion
    }
}