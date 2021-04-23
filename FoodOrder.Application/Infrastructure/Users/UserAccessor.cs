using System;
using System.Security.Claims;
using FoodOrder.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FoodOrder.Application.Infrastructure.Users
{
    public class UserAccessor :IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasPermission(Guid userId) => IsAdmin(userId) || HasPermissionToChange(userId);

        public bool IsAdmin(Guid userId)
        {
               return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value == "cem.altinay@outlook.com";
        }

        public bool HasPermissionToChange(Guid userId)
        {
            string _userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Guid.TryParse(_userId, out Guid result) ? result == userId : false;
        }
    }
}