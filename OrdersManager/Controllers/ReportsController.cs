using Microsoft.AspNetCore.Mvc;
using OrdersManager.Interfaces;
using OrdersManager.ViewModels;

namespace OrdersManager.Controllers;

public class ReportsController : Controller
{
    private readonly IReportsRepository _reportsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReportsController(IReportsRepository reportsRepository, IHttpContextAccessor httpContextAccessor)
    {
        _reportsRepository = reportsRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<IActionResult> Index(DateTime start, DateTime end)
    {

        var userOrders = await _reportsRepository.GetAllCompletedUserOrders(start, end);
        //var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
        //var user = await _reportsRepository.GetUserById(curUser);

        Decimal totalPayment = 0;
        foreach (var item in userOrders)
        {
            totalPayment += item.Payment;
        }
        var reportsViewModel = new ReportsViewModel()
        {
            Orders = userOrders,
            TotalPayment = totalPayment
        };
        return View(reportsViewModel);
    }
    

}