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
using Shared.Dtos;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.OrderItem.Queries
{

    public class GetOrderItemById : IRequest<ServiceResponse<OrderItemDto>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetOrderItemById, ServiceResponse<OrderItemDto>>
        {
            private readonly IGenericRepository<Domain.Entities.OrderItem> _orderItemRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.OrderItem> orderItemRepository, IMapper mapper)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<OrderItemDto>> Handle(GetOrderItemById request, CancellationToken cancellationToken)
            {
                try
                {
                    var dbOrderItem = await _orderItemRepository.GetByIdAsync(request.Id);
                    if (dbOrderItem is null)
                        throw new System.Exception("order Item not faund");
                    var orderItem = _mapper.Map<OrderItemDto>(dbOrderItem);
                    return new ServiceResponse<OrderItemDto>()
                    {
                        Value = orderItem,
                    };
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}