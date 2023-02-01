using Microsoft.EntityFrameworkCore;
using OrdersManager.Data;
using OrdersManager.Data.Enum;
using OrdersManager.Interfaces;
using OrdersManager.Models;

namespace OrdersManager.Repository;

public class ReportsRepository: IReportsRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReportsRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<List<Order>> GetAllCompletedUserOrders(DateTime start, DateTime end)
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userOrders = _context.Orders.Include(c => c.Client)
            .Include(s => s.Shop)
            .Where(o => o.status == Status.Done && o.EndTime <= end && o.EndTime >= start && o.Booster.Id == curUser);
        return userOrders.ToList();
    }

    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

}