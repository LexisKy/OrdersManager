using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Data.Enum;
using OrdersManager.Models;

namespace OrdersManager.ViewModels;

public class OrderProcessViewModel
{
    public AppUser? Booster { get; set; }
    [Required]
    public Client Client { get; set; }
    public Shop? Shop { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndTime { get; set; }
    [Required]
    [Precision(18, 2)]
    public decimal Payment { get; set; }
    [Required]
    public Status status { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string? BoosterId { get; set; }
    public IEnumerable<AppUser>? Boosters { get; set; }
    [Required]
    public int? ShopId { get; set; }

    public IEnumerable<Shop>? Shops { get; set; }

}