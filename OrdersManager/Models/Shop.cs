using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace OrdersManager.Models;

public class Shop
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Url { get; set; }
    
    public string Image { get; set; }
    
}