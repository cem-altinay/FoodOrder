using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using FoodOrder.Client.Utils;
using FoodOrder.Shared.CustomException;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.ResponseModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FoodOrder.Client.Pages.PageProcess
{
    public class Supplier : ComponentBase
    {

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager UrlNavigationManager { get; set; }

        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        [Inject]
        protected ISyncLocalStorageService LocalStorageSync { get; set; }

        [Inject]
        ModalManager ModalManager { get; set; }

        // javascript çalıştırmaya yarar
        [Inject]
        IJSRuntime jsRuntime { get; set; }



        protected List<SupplierDto> SupplierList;

        protected override async Task OnInitializedAsync()
        {
            await ReLoadList();
        }

        public void GoCreateSupplier()
        {
            UrlNavigationManager.NavigateTo("/suppliers/add");
        }

        public void GoEditOrder(Guid SupplierId)
        {
            UrlNavigationManager.NavigateTo("/suppliers/edit/" + SupplierId.ToString());

        }

        public async Task ReLoadList()
        {
            var res = await Http.GetFromJsonAsync<ServiceResponse<List<SupplierDto>>>($"api/supplier/allsuppliers");

            SupplierList = res.Success && res.Value != null ? res.Value : new List<SupplierDto>();
        }

        public async Task DeleteSupplier(Guid id)
        {
            var modalRes = await ModalManager.ConfirmationPopup("Confirm", "Supplier will be deleted. Are you sure?");
            if (!modalRes)
                return;

            try
            {
                var res = await Http.PostGetBaseResponseAsync("api/supplier/delete", id);

                if (res.Success)
                {
                    SupplierList.RemoveAll(i => i.Id == id);
                    //await loadList();
                }
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("Error", ex.Message);
            }
            catch (Exception ex)
            {
                await ModalManager.ShowMessageAsync("An Error", ex.Message);
            }
        }

        public async void GoWebUrl(Uri Url)
        {
            await jsRuntime.InvokeAsync<object>("open", Url.ToString(), "_blank");
        }
    }
}