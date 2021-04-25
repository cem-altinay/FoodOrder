using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace FoodOrder.Client.Utils
{
    public static class LocalStorageExtension
    {
         public async static Task<Guid> GetUserId(this ILocalStorageService LocalStorage)
        {
            string userGuid = await LocalStorage.GetItemAsStringAsync("userid");

            return Guid.TryParse(userGuid, out Guid UserId) ? UserId : Guid.Empty;
        }

        public static Guid GetUserIdSync(this ISyncLocalStorageService LocalStorage)
        {
            String userGuid = LocalStorage.GetItemAsString("userid");

            return Guid.TryParse(userGuid, out Guid UserId) ? UserId : Guid.Empty;
        }

        public async static Task<String> GetUserEMail(this ILocalStorageService LocalStorage)
        {
            return await LocalStorage.GetItemAsStringAsync("email");
        }

        public async static Task<String> GetUserFullName(this ILocalStorageService LocalStorage)
        {
            return await LocalStorage.GetItemAsStringAsync("fullname");
        }
    }
}