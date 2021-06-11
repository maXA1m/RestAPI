using System;
namespace NetCoreApi.Model.Users
{
    public class UserWithTokenModel
    {
        public UserModel User { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
