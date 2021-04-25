using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces.Repositories;
using FoodOrder.Application.Utils;
using FoodOrder.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;
using FoodOrder.Shared.CustomException;

namespace FoodOrder.Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<ServiceResponse<UserDto>>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, ServiceResponse<UserDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Users> _userRepository;
            public CreateUserCommandHandle(IMapper mapper, IGenericRepository<Users> userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<ServiceResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<Domain.Entities.Users>(request);
                if (user is null)
                    throw new ApiException("user not mapping");

                var dbUser = await _userRepository.TableNoTracking.FirstOrDefaultAsync(r => r.Email == request.Email, cancellationToken);
                if (dbUser != null)
                    throw new ApiException("User already exists");

                user.IsActive=true;
                user.Password=PasswordEncrypter.Encrypt(user.Password);

               await _userRepository.InsertAsync(user);


                return new ServiceResponse<UserDto>(){Value=_mapper.Map<UserDto>(user)};
            }
        }
    }
}