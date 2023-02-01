using System.ComponentModel.DataAnnotations;
using OrdersManager.Models;

namespace OrdersManager.ViewModels;

public class CreateUserViewModel
{
    public IFormFile? Image { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}