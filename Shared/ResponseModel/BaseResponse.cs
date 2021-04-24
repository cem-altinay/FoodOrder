using System;
using System.Collections.Generic;

namespace FoodOrder.Shared.ResponseModel
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; set; }

        public void SetException(Exception ex)
        {
            Success = false;
            Message = ex.Message;
        }
    }

}