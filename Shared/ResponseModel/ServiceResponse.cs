namespace FoodOrder.Shared.ResponseModel
{
    public class ServiceResponse<T>: BaseResponse
    {
        public T Value { get; set; }
    }
}