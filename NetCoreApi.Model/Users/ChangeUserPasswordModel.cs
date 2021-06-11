using System.ComponentModel.DataAnnotations;

namespace NetCoreApi.Model.Users
{
    public class ChangeUserPasswordModel
    {
        [Required]
        public string Password { get; set; }
    }
}
