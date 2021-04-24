using System;
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

namespace FoodOrder.Application.Features.User.Commands
{
    public class UpdateUserCommand : IRequest<ServiceResponse<UserDto>>
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }


        public class UpdateUserCommandHandle : IRequestHandler<UpdateUserCommand, ServiceResponse<UserDto>>
        {
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Users> _userRepository;
            public UpdateUserCommandHandle(IMapper mapper, IGenericRepository<Users> userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<ServiceResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
      

                var dbUser = await _userRepository.GetByIdAsync(request.Id);
                if (dbUser is null)
                    throw new System.Exception("User not faund");

             
                dbUser.Password = PasswordEncrypter.Encrypt(request.Password);
                dbUser.Email=request.Email;
                dbUser.FirstName=request.FirstName;
                dbUser.IsActive=request.IsActive;
                dbUser.LastName=request.LastName;
                
                //_mapper.Map(request, dbUser);
                await _userRepository.UpdateAsync(dbUser);


                return new ServiceResponse<UserDto>() { Value = _mapper.Map<UserDto>(dbUser) };
            }

          
        }
    }
}