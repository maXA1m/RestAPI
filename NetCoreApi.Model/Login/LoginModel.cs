using System.ComponentModel.DataAnnotations;

namespace NetCoreApi.Model.Login
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
