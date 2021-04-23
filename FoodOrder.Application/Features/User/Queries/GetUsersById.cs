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
using Shared.Dtos;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.User.Queries
{
    public class GetUsers
    {
        public class Query : IRequest<ServiceResponse<List<UserDto>>>
        {

        }

        public class Handler : IRequestHandler<Query, ServiceResponse<List<UserDto>>>
        {
            private readonly IGenericRepository<Users> _userRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Users> userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var users = await _userRepository.TableNoTracking.Where(r => r.IsActive)
                                                               .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                                               .ToListAsync(cancellationToken);
                    return new ServiceResponse<List<UserDto>>()
                    {
                        Value = users,
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