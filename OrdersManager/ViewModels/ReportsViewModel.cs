using Microsoft.EntityFrameworkCore;
using OrdersManager.Models;

namespace OrdersManager.ViewModels;

public class ReportsViewModel
{
    public List<Order> Orders { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    [Precision(18, 2)]
    public Decimal TotalPayment { get; set; }
}