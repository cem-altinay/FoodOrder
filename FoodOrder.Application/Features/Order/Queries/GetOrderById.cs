using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;

namespace FoodOrder.Application.Features.Order.Queries
{
    public class GetOrderById : IRequest<ServiceResponse<OrderDto>>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetOrderById, ServiceResponse<OrderDto>>
        {
            private readonly IGenericRepository<Domain.Entities.Order> _orderRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.Order> orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<OrderDto>> Handle(GetOrderById request, CancellationToken cancellationToken)
            {
               
                    var dbOrder = await _orderRepository.GetByIdAsync(request.Id);
                    if (dbOrder is null)
                        throw new System.Exception("Order not faund");

                    var order = _mapper.Map<OrderDto>(dbOrder);

                    return new ServiceResponse<OrderDto>()
                    {
                        Value = order,
                    };
            
            }
        }
    }
}