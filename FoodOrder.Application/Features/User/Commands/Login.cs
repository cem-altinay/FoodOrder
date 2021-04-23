using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoodOrder.Application.Interfaces;
using FoodOrder.Application.Interfaces.Repositories;
using FoodOrder.Application.Utils;
using FoodOrder.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.User.Commands
{
    public class LoginCommand : IRequest<ServiceResponse<UserLoginDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public class LoginCommandHandle : IRequestHandler<LoginCommand, ServiceResponse<UserLoginDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Users> _userRepository;
            private readonly IJWTGenerator _JWTGenerator;
            public LoginCommandHandle(IMapper mapper, IGenericRepository<Users> userRepository, IJWTGenerator jWTGenerator)
            {
                _mapper = mapper;
                _userRepository = userRepository;
                _JWTGenerator = jWTGenerator;
            }

            public async Task<ServiceResponse<UserLoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var encryptedPassword = PasswordEncrypter.Encrypt(request.Password);

                var dbUser = await _userRepository.TableNoTracking.FirstOrDefaultAsync(r => r.Email == request.Email && r.Password == encryptedPassword, cancellationToken);

                if (dbUser is null)
                    throw new Exception("User not found or given information is wrong");

                if (!dbUser.IsActive)
                    throw new Exception("User state is Passive!");

                var user = _mapper.Map<UserDto>(dbUser);
                UserLoginDto userLogin = _JWTGenerator.CreateToken(dbUser);
                userLogin.User = user;

                return new ServiceResponse<UserLoginDto>() { Value = userLogin };
            }


        }
    }
}