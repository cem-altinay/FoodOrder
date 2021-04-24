using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;

namespace FoodOrder.Application.Features.Order.Commands
{
    public class DeleteOrderCommand : IRequest<ServiceResponse<bool>>
    {

        public Guid Id { get; set; }


        public class DeleteOrderCommandHandle : IRequestHandler<DeleteOrderCommand, ServiceResponse<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.Order> _orderRepository;
            private readonly IUserAccessor _userAccessor;
            public DeleteOrderCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.Order> orderRepository, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _orderRepository = orderRepository;
                _userAccessor = userAccessor;
            }

            public async Task<ServiceResponse<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {

                var order = await _orderRepository.Table.Include(inc => inc.OrderItems)
                                                              .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                if (order is null)
                    throw new System.Exception("Order not found");

                if (!_userAccessor.HasPermission(order.CreatedUserId))
                    throw new Exception("You cannot change the order unless you created");

                var orderCount = order.OrderItems.Count();
                if (orderCount > 0)
                    throw new Exception($"There are {orderCount} sub order item for the order item you are trying to delete");

                await _orderRepository.DeleteAsync(order);

                return new ServiceResponse<bool>() { Value = true };
            }
        }
    }

}