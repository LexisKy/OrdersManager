using OrdersManager.Models;

namespace OrdersManager.Interfaces;

public interface IShopRepository
{
    Task<IEnumerable<Shop>> GetAll();
    Task<Shop> GetByIdAsync(int id);
    bool Add(Shop shop);
    bool Update(Shop shop);
    bool Delete(Shop shop);
    bool Save();

}