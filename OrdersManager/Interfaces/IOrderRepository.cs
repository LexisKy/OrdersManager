using OrdersManager.Data.Enum;
using OrdersManager.Models;

namespace OrdersManager.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAll();
    Task<Order> GetByIdAsync(int id);
    Task<Order> GetByIdAsyncNoTracking(int id);
    IQueryable<Order> GetAllOrdersWithCurrentStatus(Status status);
    bool Add(Order order);
    bool Update(Order order);
    bool Delete(Order order);
    bool Save();
    IQueryable<Order> GetAllNew();
}