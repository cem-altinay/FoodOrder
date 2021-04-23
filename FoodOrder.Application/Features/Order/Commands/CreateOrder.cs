using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.Order.Commands
{
    public class CreateOrderCommand : IRequest<ServiceResponse<OrderDto>>
    {

        public Guid CreatedUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }


        public class CreateOrderCommandHandle : IRequestHandler<CreateOrderCommand, ServiceResponse<OrderDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.Order> _orderRepository;
            public CreateOrderCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.Order> orderRepository)
            {
                _mapper = mapper;
                _orderRepository = orderRepository;
            }

            public async Task<ServiceResponse<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = _mapper.Map<Domain.Entities.Order>(request);
                if (order is null)
                    throw new System.Exception("order not mapping");

                await _orderRepository.InsertAsync(order);

                return new ServiceResponse<OrderDto>() { Value = _mapper.Map<OrderDto>(order) };
            }
        }
    }

}