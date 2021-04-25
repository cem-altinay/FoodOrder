using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodOrder.Application.Interfaces.Repositories;
using FoodOrder.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;
using FoodOrder.Shared.CustomException;

namespace FoodOrder.Application.Features.User.Queries
{
    public class GetUsersById
    {
        public class Query : IRequest<ServiceResponse<UserDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ServiceResponse<UserDto>>
        {
            private readonly IGenericRepository<Users> _userRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Users> userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
               
                    var dbUser = await _userRepository.GetByIdAsync(request.Id);
                    if (dbUser is null)
                        throw new ApiException("User not faund");

                    var user = _mapper.Map<UserDto>(dbUser);

                    return new ServiceResponse<UserDto>()
                    {
                        Value = user,
                    };
               
            }
        }
    }
}