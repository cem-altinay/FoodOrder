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
    public class UpdateOrderItemCommand : IRequest<ServiceResponse<OrderItemDto>>
    {
        public Guid Id { get; set; }
        public Guid UpdatedUserId { get; set; }
        public Guid OrderId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


        public class UpdateOrderItemCommandHandle : IRequestHandler<UpdateOrderItemCommand, ServiceResponse<OrderItemDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.OrderItem> _orderItemRepository;
            public UpdateOrderItemCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.OrderItem> orderItemRepository)
            {
                _mapper = mapper;
                _orderItemRepository = orderItemRepository;
            }

            public async Task<ServiceResponse<OrderItemDto>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var dbOrderItem = await _orderItemRepository.GetByIdAsync(request.Id);
                if (dbOrderItem == null)
                    throw new ApiException("Order item not found");


                _mapper.Map(request, dbOrderItem);
                await _orderItemRepository.UpdateAsync(dbOrderItem);

                return new ServiceResponse<OrderItemDto>() { Value = _mapper.Map<OrderItemDto>(dbOrderItem) };
            }
        }
    }

}