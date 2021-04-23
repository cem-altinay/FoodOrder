using FoodOrder.Domain.Entities;
using Shared.Dtos;

namespace FoodOrder.Application.Interfaces
{
    public interface IJWTGenerator
    {
        UserLoginDto CreateToken(Users user);
    }
}