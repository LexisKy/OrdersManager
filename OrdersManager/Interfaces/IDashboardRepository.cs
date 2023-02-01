using OrdersManager.Data.Enum;
using OrdersManager.Models;

namespace OrdersManager.Interfaces;

public interface IDashboardRepository
{
    Task<List<Order>> GetAllUserOrders();
    Task<List<Order>> GetAllUserOrdersWithStatus(Status status);
    Task<AppUser> GetUserById(string id);
}