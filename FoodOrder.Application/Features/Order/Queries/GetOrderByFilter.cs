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
    public class GetOrderByFilter : IRequest<ServiceResponse<List<OrderDto>>>
    {

        public DateTime? CreateDateFirst { get; set; }

        public DateTime CreateDateLast { get; set; }

        public Guid CreatedUserId { get; set; }

        public class Handler : IRequestHandler<GetOrderByFilter, ServiceResponse<List<OrderDto>>>
        {
            private readonly IGenericRepository<Domain.Entities.Order> _orderRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.Order> orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<OrderDto>>> Handle(GetOrderByFilter request, CancellationToken cancellationToken)
            {
                
                    var query = _orderRepository.TableNoTracking.Where(r => r.IsActive);

                    if (request.CreatedUserId != Guid.Empty)
                        query = query.Where(i => i.CreatedUserId == request.CreatedUserId);

                    if (request.CreateDateFirst.HasValue)
                        query = query.Where(i => i.CreateDate >= request.CreateDateFirst);

                    if (request.CreateDateLast > DateTime.MinValue)
                        query = query.Where(i => i.CreateDate <= request.CreateDateLast);


                    var orders = await query
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