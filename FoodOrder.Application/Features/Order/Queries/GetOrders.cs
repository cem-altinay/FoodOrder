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
    public class GetOrders : IRequest<ServiceResponse<List<OrderDto>>>
    {
        public DateTime? OrderDate { get; set; }
        public class Handler : IRequestHandler<GetOrders, ServiceResponse<List<OrderDto>>>
        {
            private readonly IGenericRepository<Domain.Entities.Order> _orderRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.Order> orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<OrderDto>>> Handle(GetOrders request, CancellationToken cancellationToken)
            {
               
                    var ordersQuery = _orderRepository.TableNoTracking.Where(r => r.IsActive);

                    if (request.OrderDate.HasValue)
                        ordersQuery = ordersQuery.Where(r => r.CreateDate.Date == request.OrderDate.Value.Date);


                    var orders = await ordersQuery
                                            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                                            .OrderBy(ord => ord.CreateDate)
                                            .ToListAsync(cancellationToken);

                    return new ServiceResponse<List<OrderDto>>()
                    {
                        Value = orders,
                    };
               
            }
        }
    }
}