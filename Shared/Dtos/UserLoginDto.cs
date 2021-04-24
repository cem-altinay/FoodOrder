namespace FoodOrder.Shared.Dtos
{
    public class UserLoginDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}