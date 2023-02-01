using OrdersManager.Models;

namespace OrdersManager.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetAllUsers();
    Task<AppUser> GetUserById(string id);
    bool Update(AppUser user);
    bool Delete(AppUser user);
    Task<AppUser> GetByIdNoTracking(string id);
}