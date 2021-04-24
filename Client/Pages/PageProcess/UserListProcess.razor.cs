using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Shared.ResponseModel;
using System.Collections.Generic;
using FoodOrder.Shared.Dtos;

namespace Client.Pages.PageProcess
{
    public  class UserListProcess :ComponentBase
    {
        [Inject]
        public HttpClient Client {get;set;}

        protected List<UserDto> Users = new();



        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }
       

       protected async Task LoadList()
       {
          var serviceResponse = await Client.GetFromJsonAsync<ServiceResponse<List<UserDto>>>("api/user/allusers") ;
            if (serviceResponse.Success)
                Users = serviceResponse.Value;
       }
    }
}