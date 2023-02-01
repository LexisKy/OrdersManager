using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Data;
using OrdersManager.Data.Enum;
using OrdersManager.Interfaces;
using OrdersManager.Models;
using OrdersManager.ViewModels;

namespace OrdersManager.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _context.Orders.Include(x => x.Booster).Include(y => y.Shop).ToListAsync();
    }

    public IQueryable<Order> GetAllNew()
    {
        return _context.Orders.Include(x => x.Booster).Include(y => y.Shop);
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders.Include(i => i.Booster).Include(i => i.Shop).Include(i => i.Client).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Order> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Orders.Include(i => i.Booster).Include(i => i.Shop).Include(i => i.Client).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public bool Add(Order order)
    {
        _context.Add(order);
        return Save();
    }

    public bool Update(Order order)
    {
        _context.Update(order);
        return Save();
    }

    public bool Delete(Order order)
    {
        _context.Remove(order);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
    
    public IQueryable<Order> GetAllOrdersWithCurrentStatus(Status status)
    {
        return _context.Orders.Include(x => x.Booster).Include(y => y.Shop)
            .Include(c => c.Client).Where(s=>s.status == status);
    }
    
}