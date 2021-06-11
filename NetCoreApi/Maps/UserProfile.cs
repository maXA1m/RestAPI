using AutoMapper;
using NetCoreApi.Data.Model;
using NetCoreApi.Model.Users;

namespace NetCoreApi.Maps
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>();
        }
    }
}
