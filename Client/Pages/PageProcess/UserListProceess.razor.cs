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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected List<UserDto> Users;



        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }


        protected async Task LoadList()
        {
            try
            {
                //var serviceResponse = await Client.GetFromJsonAsync<ServiceResponse<List<UserDto>>>("api/user/allusers");
                Users = await Client.GetServiceResponseAsync<List<UserDto>>("api/user/allusers", true);

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

        protected void goCreateUserPage()
        {
            NavigationManager.NavigateTo("/users/add");
        }

        protected void goUpdateUserPage(Guid userId)
        {
            NavigationManager.NavigateTo("/users/edit/" + userId);
        }

        protected async Task DeleteUser(Guid Id)
        {
            bool confirmed = await ModalManager.ConfirmationPopup("Confirmation", "User will be deleted. Are you sure?");

            if (!confirmed) return;

            try
            {
                bool deleted = await Client.PostGetServiceResponseAsync<bool, Guid>("api/User/Delete", Id, true);

                await LoadList();
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("User Deletion Error", ex.Message);
            }
            catch (Exception ex)
            {
                await ModalManager.ShowMessageAsync("An Error", ex.Message);
            }
        }

    }
}
