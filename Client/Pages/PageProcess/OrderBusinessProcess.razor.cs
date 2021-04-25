using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using FoodOrder.Client.Utils;
using FoodOrder.Shared.CustomException;
using FoodOrder.Shared.Dtos;
using FoodOrder.Shared.FilterModels;
using FoodOrder.Shared.ResponseModel;
using Microsoft.AspNetCore.Components;

namespace FoodOrder.Client.Pages.PageProcess
{
     public class OrderBusiness : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        [Inject]
        protected ISyncLocalStorageService LocalStorageSync { get; set; }


        [Inject]
        ModalManager ModalManager { get; set; }

        public OrderListFilterModel filterModel = new OrderListFilterModel() { CreateDateFirst = DateTime.Now.Date, CreateDateLast = DateTime.Now.Date };

        protected List<OrderDto> OrderList;

        internal bool loading;

        protected override async Task OnInitializedAsync()
        {
            await ReLoadList();
        }

        protected String GetRemaningDateStr(DateTime ExpireDate)
        {
            TimeSpan ts = ExpireDate.Subtract(DateTime.Now);

            return ts.TotalSeconds >= 0 ? $"{ts.Hours}:{ts.Minutes}:{ts.Seconds}" : "00:00:00";
        }

        public void GoDetails(Guid SelectedOrderId)
        {
            NavigationManager.NavigateTo("/orders-items/" + SelectedOrderId.ToString());
        }


        public void GoCreateOrder()
        {
            NavigationManager.NavigateTo("/orders/add");
        }

        public void GoEditOrder(Guid OrderId)
        {
            NavigationManager.NavigateTo("/orders/edit/" + OrderId.ToString());
        }

        public async Task ReLoadList()
        {
            loading = true;

            try
            {
                OrderList = await Http.PostGetServiceResponseAsync<List<OrderDto>, OrderListFilterModel>("api/order/ordersbyfilter", filterModel, true);
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("List Error", ex.Message);
            }
            finally
            {
                loading = false;
            }
        }

        public bool IsExpired(DateTime ExpireDate)
        {
            TimeSpan ts = ExpireDate.Subtract(DateTime.Now);
            return ts.TotalSeconds < 0;
        }

        public async Task DeleteOrder(Guid id)
        {
            try
            {
                var modalRes = await ModalManager.ConfirmationPopup("Confirm", "Order will be deleted. Are you sure?");
                if (!modalRes)
                    return;

                var res = await Http.GetServiceResponseAsync<BaseResponse>("api/Order/DeleteOrder/" + id, true);
                
                OrderList.RemoveAll(i => i.Id == id);
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("Deletion Error", ex.Message);
            }
        }

        public bool IsMyOrder(Guid CreatedUserId)
        {
            return LocalStorageSync.GetUserIdSync() == CreatedUserId;
        }
    }
}