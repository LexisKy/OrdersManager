using OrdersManager.Data.Enum;
using OrdersManager.ViewModels;

namespace OrdersManager.Models;

public class IndexViewModel
{
    public IEnumerable<Order> Orders { get; set; }
    public PageViewModel PageViewModel { get; set; }
    public Status Status { get; set; }
}