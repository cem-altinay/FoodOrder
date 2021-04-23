using System.Threading;
using System.Threading.Tasks;
using FoodOrder.Application.Interfaces.Repositories;
using FoodOrder.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.ResponseModel;

namespace FoodOrder.Application.Features.User.Commands
{
    public class DeleteUserCommand : IRequest<ServiceResponse<bool>>
    {
        public System.Guid Id { get; set; }

           public class DeleteUserCommandHandle : IRequestHandler<DeleteUserCommand, ServiceResponse<bool>>
        {
           
            private readonly IGenericRepository<Users> _userRepository;
            public DeleteUserCommandHandle( IGenericRepository<Users> userRepository)
            {
            
                _userRepository = userRepository;
            }

            public async Task<ServiceResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
             

                var dbUser = await _userRepository.GetByIdAsync(request.Id);
                if (dbUser is null)
                    throw new System.Exception("User not faund");

                dbUser.IsActive=false;


               await _userRepository.DeleteAsync(dbUser);


                return new ServiceResponse<bool>(){Value=true};
            }
        }
    }
}