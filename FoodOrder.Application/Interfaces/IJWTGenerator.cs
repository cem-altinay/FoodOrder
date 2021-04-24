using FoodOrder.Domain.Entities;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;
namespace FoodOrder.Application.Interfaces
{
    public interface IJWTGenerator
    {
        UserLoginDto CreateToken(Users user);
    }
}