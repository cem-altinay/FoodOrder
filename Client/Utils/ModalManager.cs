using System;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using FoodOrder.Client.CustomComponents.ModalComponent;

namespace FoodOrder.Client.Utils
{
    public class ModalManager
    {

        public IModalService _modalService { get; }
        public ModalManager(IModalService modalService)
        {
            _modalService = modalService;
        }

    

        public async Task ShowMessageAsync(string title, string message, int duration=0)
        {
            ModalParameters mParams = new ModalParameters();
            mParams.Add("Message", message);
           var modalRef= _modalService.Show<ShowMessagePupupComponent>(title, mParams);

            if (duration > 0)
            {
                await Task.Delay(duration);
                modalRef.Close();
            }
            //await modalRef.Result;
        }

        public async Task<bool> ConfirmationPopup(string title, string message)
        {
          ModalParameters mParams = new ModalParameters();
            mParams.Add("Message", message);

            var modalRef = _modalService.Show<ConfirmationPopupComponent>(title, mParams);
           var result = await modalRef.Result;

            return !result.Cancelled;
        }

        
            

    }
}
