using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;

namespace FoodOrder.Application.Features.OrderItem.Commands
{
    public class DeleteOrderItemCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }


        public class DeleteOrderItemCommandHandle : IRequestHandler<DeleteOrderItemCommand, ServiceResponse<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.OrderItem> _orderItemRepository;
            public DeleteOrderItemCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.OrderItem> orderItemRepository)
            {
                _mapper = mapper;
                _orderItemRepository = orderItemRepository;
            }

            public async Task<ServiceResponse<bool>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
            {
                var dbOrderItem = await _orderItemRepository.GetByIdAsync(request.Id);
                if (dbOrderItem == null)
                    throw new Exception("Order item not found");

                await _orderItemRepository.DeleteAsync(dbOrderItem);

                return new ServiceResponse<bool>() { Value = true };
            }
        }
    }

}