using System.ComponentModel.DataAnnotations;

namespace LiquorStoreFinalProject.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email or username is required")]
        public string EmailOrUsername { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
