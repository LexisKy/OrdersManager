using OrdersManager.Models;

namespace OrdersManager.Interfaces;

public interface IReportsRepository
{
    Task<List<Order>> GetAllCompletedUserOrders(DateTime start, DateTime end);
    Task<AppUser> GetUserById(string id);
    
}