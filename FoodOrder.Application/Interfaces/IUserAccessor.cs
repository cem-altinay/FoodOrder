using System;

namespace FoodOrder.Application.Interfaces
{
    public interface IUserAccessor
    {
        bool HasPermission(Guid userId);
        bool HasPermissionToChange(Guid userId);
        bool IsAdmin(Guid userId);
    }
}