using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Data.Enum;

namespace OrdersManager.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public AppUser? Booster { get; set; }
    public Client Client { get; set; }
    public Shop Shop { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndTime { get; set; }
    
    [Precision(18, 2)]
    public decimal Payment { get; set; }
    public Status status { get; set; }
    
    public string Description { get; set; }
    
}