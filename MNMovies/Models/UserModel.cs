using System.ComponentModel.DataAnnotations;

namespace MNMovies.Models
{
    public class UserModel
    {
        [Key]
        public int? UserID { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string MobileNo { get; set; } = string.Empty;

        public bool Role {  get; set; }
    }

    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class UserLoginResponseModel
    {
        public string Token { get; set; }
        public UserModel User { get; set; }
    }

    public class UserRegisterModel
    {
        [Key]
        public int? UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }

        public bool Role { get; set; }
    }
}
