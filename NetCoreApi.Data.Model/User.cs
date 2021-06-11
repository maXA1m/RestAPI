using System.Collections.Generic;

namespace NetCoreApi.Data.Model
{
    public class User : EntityBase
    {
        public User()
        {
            Roles = new List<UserRole>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual IList<UserRole> Roles { get; set; }
    }
}
