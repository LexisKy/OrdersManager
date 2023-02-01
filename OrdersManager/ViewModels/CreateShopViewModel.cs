namespace OrdersManager.ViewModels;

public class CreateShopViewModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Url { get; set; }
    
    public IFormFile Image { get; set; }
}