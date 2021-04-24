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

namespace FoodOrder.Application.Features.OrderItem.Queries
{

    public class GetOrderItems : IRequest<ServiceResponse<List<OrderItemDto>>>
    {
        public Guid orderId { get; set; }
        public class Handler : IRequestHandler<GetOrderItems, ServiceResponse<List<OrderItemDto>>>
        {
            private readonly IGenericRepository<Domain.Entities.OrderItem> _orderItemRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.OrderItem> orderItemRepository, IMapper mapper)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<OrderItemDto>>> Handle(GetOrderItems request, CancellationToken cancellationToken)
            {
               
                    var ordersQuery = _orderItemRepository.TableNoTracking.Where(r => r.IsActive);

                    var orders = await ordersQuery
                                            .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                                            .OrderBy(ord => ord.CreateDate)
                                            .ToListAsync(cancellationToken);

                    return new ServiceResponse<List<OrderItemDto>>()
                    {
                        Value = orders,
                    };
             
            }
        }
    }
}