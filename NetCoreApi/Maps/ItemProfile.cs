using AutoMapper;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Items;

namespace NetCoreApi.Maps
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemModel>();
        }
    }
}
