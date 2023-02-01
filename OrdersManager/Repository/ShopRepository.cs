using Microsoft.EntityFrameworkCore;
using OrdersManager.Data;
using OrdersManager.Interfaces;
using OrdersManager.Models;

namespace OrdersManager.Repository;

public class ShopRepository : IShopRepository
{
    private readonly ApplicationDbContext _context;
    public ShopRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<IEnumerable<Shop>> GetAll()
    {
        return await _context.Shops.ToListAsync();
    }

    public async Task<Shop> GetByIdAsync(int id)
    {
        return await _context.Shops.FirstOrDefaultAsync(i => i.Id == id);
    }

    public bool Add(Shop shop)
    {
        _context.Add(shop);
        return Save();
    }

    public bool Update(Shop shop)
    {
        _context.Update(shop);
        return Save();
    }

    public bool Delete(Shop shop)
    {
        _context.Remove(shop);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

}