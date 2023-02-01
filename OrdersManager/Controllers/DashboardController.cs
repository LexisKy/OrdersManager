using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.Data.Enum;
using OrdersManager.Interfaces;
using OrdersManager.ViewModels;

namespace OrdersManager.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor)
    {
        _dashboardRepository = dashboardRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userOrders = await _dashboardRepository.GetAllUserOrders();
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var user = await _dashboardRepository.GetUserById(curUser);
        var dashboardViewModel = new DashboardViewModel()
        {
            Orders = userOrders,
            User = user,
            Start = DateTime.Today,
            End = DateTime.Today
        };
        return View(dashboardViewModel);
    }

    [Authorize]
    public async Task<IActionResult> OrdersWithCurrentStatus(Status status)
    {
        var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        var userOrders = await _dashboardRepository.GetAllUserOrdersWithStatus(status);
        var user = await _dashboardRepository.GetUserById(curUser);
        var ordersResult = new DashboardViewModel()
        {
            Orders = userOrders
        };

        return View(ordersResult);
    }
}