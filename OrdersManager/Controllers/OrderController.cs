using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Data;
using OrdersManager.Data.Enum;
using OrdersManager.Interfaces;
using OrdersManager.Models;
using OrdersManager.ViewModels;

namespace OrdersManager.Controllers;

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IShopRepository _shopRepository;

    public OrderController(ApplicationDbContext context, IOrderRepository orderRepository, IUserRepository userRepository
    ,IShopRepository shopRepository)
    {
        _context = context;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _shopRepository = shopRepository;
    }
    
    [Authorize(Roles="admin")]
    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 5;
        IQueryable<Order> source = _orderRepository.GetAllNew();
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
 
        PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        IndexViewModel viewModel = new IndexViewModel
        {
            PageViewModel = pageViewModel,
            Orders = items
        };
        
        return View(viewModel);
    }

    [Authorize]
    public async Task<IActionResult> OrdersWithCurrentStatus(Status status, int page = 1)
    {
        int pageSize = 5;
        var source= _orderRepository.GetAllOrdersWithCurrentStatus(status);
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        
        PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        IndexViewModel viewModel = new IndexViewModel ()
        {
            PageViewModel = pageViewModel,
            Orders = items
        };

        return View(viewModel);
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        Order order = await _orderRepository.GetByIdAsync(id);

        return View(order);
    }

    [Authorize(Roles="admin")]
    public async Task<IActionResult> Create()
    {
        var orderVM = new OrderProcessViewModel()
        {
            Boosters = await _userRepository.GetAllUsers(),
            Shops = await _shopRepository.GetAll()
        };
        return View(orderVM);
    }

    [Authorize(Roles="admin")]
    [HttpPost]
    public async Task<IActionResult> Create(OrderProcessViewModel orderVM)
    {
        
        if (!ModelState.IsValid)
        {
            orderVM.Boosters = await _userRepository.GetAllUsers();
            orderVM.Shops = await _shopRepository.GetAll();
            return View(orderVM);
        }
        
        AppUser appUser = _context.AppUsers.Where(c => c.Id == orderVM.BoosterId).FirstOrDefault();
        if (appUser == null)
        {
            return View(orderVM);
        }
        Shop shop = _context.Shops.Where(c => c.Id == orderVM.ShopId).FirstOrDefault();
        if (shop == null)
        {
            return View(orderVM);
        }
        
        orderVM.Booster = appUser;
        orderVM.Shop = shop;
        var order = new Order()
        {
            Booster = orderVM.Booster,
            Client = orderVM.Client,
            Shop = orderVM.Shop,
            StartDate = orderVM.StartDate,
            Payment = orderVM.Payment,
            status = orderVM.status,
            Description = orderVM.Description
        };
        
        _orderRepository.Add(order);
        _orderRepository.Save();
        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Edit(int id)
    {

        var order = await _orderRepository.GetByIdAsyncNoTracking(id);

        var orderVM = new OrderProcessViewModel
        {
            Booster = order.Booster,
            Client = order.Client,
            Shop = order.Shop,
            StartDate = order.StartDate,
            Payment = order.Payment,
            status = order.status,
            Description = order.Description,
            Boosters = await _userRepository.GetAllUsers(),
            Shops = await _shopRepository.GetAll()
        };
        return View(orderVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, OrderProcessViewModel orderVM)
    {
        if (User.IsInRole("user"))
        {
            var tempOrder = _orderRepository.GetByIdAsyncNoTracking(id).Result;
            var orderFromUser = new Order
            {
                Id = id,
                Booster = tempOrder.Booster,
                Client = tempOrder.Client,
                Shop = tempOrder.Shop,
                StartDate = tempOrder.StartDate,
                Payment = tempOrder.Payment,
                status = orderVM.status,
                Description = tempOrder.Description
            };
            if (orderFromUser.status == Status.Done)
            {
                orderFromUser.EndTime = DateTime.Now;
            }
            else
            {
                orderFromUser.EndTime = null;
            }
            _orderRepository.Update(orderFromUser);
        
            return RedirectToAction("Index", "Dashboard");
        }
        
        if (!ModelState.IsValid)
        {
            Shop shop = _context.Shops.Where(c => c.Id == orderVM.ShopId).FirstOrDefault();
            orderVM.Booster = await _userRepository.GetUserById(orderVM.BoosterId);
            orderVM.Shop = shop;
            orderVM.Boosters = await _userRepository.GetAllUsers();
            orderVM.Shops = await _shopRepository.GetAll();
            return View(orderVM);
        }
        AppUser appUser = _context.AppUsers.Where(c => c.Id == orderVM.BoosterId).FirstOrDefault();
        Shop editedShop = _context.Shops.Where(c => c.Id == orderVM.ShopId).FirstOrDefault();

        var order = new Order
        {
            Id = id,
            Booster = appUser,
            Client = orderVM.Client,
            Shop = editedShop,
            StartDate = orderVM.StartDate,
            Payment = orderVM.Payment,
            status = orderVM.status,
            Description = orderVM.Description
        };
        if (order.status == Status.Done)
        {
            order.EndTime = DateTime.Now;
        }
        else
        {
            order.EndTime = null;
        }
        _orderRepository.Update(order);
        
        return RedirectToAction("Index", "Dashboard");
    }
}