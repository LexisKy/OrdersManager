using System.ComponentModel.DataAnnotations;

namespace OrdersManager.Models;

public class Client
{
    [Key]
    public int Id { get; set; }
    
    public string loginInfo { get; set; }
    public string passwordInfo { get; set; }
    public string vpnInfo { get; set; }
    public string note { get; set; }
    
}