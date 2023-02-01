using System.ComponentModel.DataAnnotations;
using ErrorEventArgs = Microsoft.AspNetCore.Components.Web.ErrorEventArgs;

namespace OrdersManager.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email Address")]
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}