using AutoMapper;
using FoodOrder.Application.Features.User.Commands;
using FoodOrder.Application.Utils;
using FoodOrder.Domain.Entities;
using Shared.Dtos;

namespace FoodOrder.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;

            CreateMap<Supplier, SupplierDto>()
                .ReverseMap();

            CreateMap<Users, UserDto>();

            CreateMap<UserDto, Users>()
                .ForMember(x => x.Password, y => y.MapFrom(z => PasswordEncrypter.Encrypt(z.Password)))
                ;

            CreateMap<Users, CreateUserCommand>().ReverseMap();
             CreateMap<Users, UpdateUserCommand>().ReverseMap();

            CreateMap<Order, OrderDto>()
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName));

            CreateMap<OrderDto, Order>();



            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName))
                .ForMember(x => x.OrderName, y => y.MapFrom(z => z.Order.Name ?? ""));

            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}