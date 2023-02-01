using System.Collections;
using OrdersManager.Data.Enum;
using OrdersManager.Models;

namespace OrdersManager.ViewModels;

public class DashboardViewModel
{
    public List<Order> Orders { get; set; }
    public AppUser? User { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Status Status { get; set; }
    public Shop Shop { get; set; }

}