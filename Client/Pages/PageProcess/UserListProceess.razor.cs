using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoodOrder.Client.Utils;
using FoodOrder.Shared.CustomException;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;
using Microsoft.AspNetCore.Components;

namespace FoodOrder.Client.Pages.PageProcess
{
    public class UserListProceess : ComponentBase
    {
        public UserListProceess()
        {
        }

        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public ModalManager ModalManager { get; set; }

        protected List<UserDto> Users;



        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }


        protected async Task LoadList()
        {
            try
            {
                var serviceResponse = await Client.GetFromJsonAsync<ServiceResponse<List<UserDto>>>("api/user/allusers");
                if (serviceResponse.Success)
                    Users = serviceResponse.Value;
            }
            catch (ApiException ex)
            {

                await ModalManager.ShowMessageAsync("Api Exception", ex.Message);
            }
            catch (Exception ex)
            {
                await ModalManager.ShowMessageAsync("Exception", ex.Message);
            }

        }
    }
}
