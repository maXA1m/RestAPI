using System;
namespace NetCoreApi.Data.Model
{
    public class UserWithToken
    {
        public User User { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
