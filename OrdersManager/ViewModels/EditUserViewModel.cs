namespace OrdersManager.ViewModels;

public class EditUserViewModel
{
    public string Id { get; set; }
    public string? ProfileImageUrl { get; set; }
    public IFormFile? Image { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? NewEmail { get; set; }
}