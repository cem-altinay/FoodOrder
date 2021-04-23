using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces;
using FoodOrder.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.Order.Commands
{
    public class UpdateOrderCommand : IRequest<ServiceResponse<OrderDto>>
    {
        public Guid Id { get; set; }
        public Guid UpdatedUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }


        public class UpdateOrderCommandHandle : IRequestHandler<UpdateOrderCommand, ServiceResponse<OrderDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Domain.Entities.Order> _orderRepository;
            private readonly IUserAccessor _userAccessor;
            public UpdateOrderCommandHandle(IMapper mapper, IGenericRepository<Domain.Entities.Order> orderRepository, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _orderRepository = orderRepository;
                _userAccessor = userAccessor;
            }

            public async Task<ServiceResponse<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var dbOrder = await _orderRepository.GetByIdAsync(request.Id);
                if (dbOrder == null)
                    throw new Exception("Order not found");


                if (!_userAccessor.HasPermission(dbOrder.CreatedUserId))
                    throw new Exception("You cannot change the order unless you created");

                _mapper.Map(request, dbOrder);
                await _orderRepository.UpdateAsync(dbOrder);

                return new ServiceResponse<OrderDto>() { Value = _mapper.Map<OrderDto>(dbOrder) };
            }
        }
    }

}