using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is Wrong")]
        [EmailAddress(ErrorMessage ="Email is Invalid")]
        public string Email { get; set; }


        [Required(ErrorMessage ="Password is Wrong")]
        [DataType(DataType.Password)]
        public string Password{ get; set; }

        public bool RememberMe { get; set; }
    }
}
