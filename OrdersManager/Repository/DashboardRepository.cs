using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Data;
using OrdersManager.Data.Enum;
using OrdersManager.Interfaces;
using OrdersManager.Models;

namespace OrdersManager.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<List<Order>> GetAllUserOrders()
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userOrders = _context.Orders
            .Include(s => s.Shop)
            .Include(b => b.Booster).Include(c => c.Client)
            .Where(r => r.Booster.Id == curUser);
        return userOrders.ToList();
    }
    
    public async Task<List<Order>> GetAllUserOrdersWithStatus(Status status)
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userOrders = _context.Orders
                .Include(s => s.Shop)
                .Include(b => b.Booster).Include(c => c.Client)
                .Where(r => r.Booster.Id == curUser && r.status == status);
        return userOrders.ToList();
    }

    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

}