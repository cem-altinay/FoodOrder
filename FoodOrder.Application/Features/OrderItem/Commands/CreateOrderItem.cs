using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;
using FoodOrder.Shared.CustomException;

namespace FoodOrder.Application.Features.OrderItem.Commands
{
    public class CreateOrderItemCommand : IRequest<ServiceResponse<OrderItemDto>>
    {
        public Guid CreatedUserId { get; set; }
        public Guid OrderId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


        public class CreateOrderItemCommandHandle : IRequestHandler<CreateOrderItemCommand, ServiceResponse<OrderItemDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.OrderItem> _orderItemRepository;
            public CreateOrderItemCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.OrderItem> orderItemRepository)
            {
                _mapper = mapper;
                _orderItemRepository = orderItemRepository;
            }

            public async Task<ServiceResponse<OrderItemDto>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var orderItem = _mapper.Map<Domain.Entities.OrderItem>(request);
                if (orderItem is null)
                    throw new ApiException("order item not mapping");

                await _orderItemRepository.InsertAsync(orderItem);

                return new ServiceResponse<OrderItemDto>() { Value = _mapper.Map<OrderItemDto>(orderItem) };
            }
        }
    }

}