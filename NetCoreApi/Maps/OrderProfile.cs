using AutoMapper;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Orders;

namespace NetCoreApi.Maps
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel>();
        }
    }
}
