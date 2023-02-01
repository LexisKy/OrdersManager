using Microsoft.AspNetCore.Mvc;
using OrdersManager.Data;
using OrdersManager.Interfaces;
using OrdersManager.Models;
using OrdersManager.ViewModels;

namespace OrdersManager.Controllers;

public class ShopController : Controller
{
    private readonly IShopRepository _shopRepository;
    private readonly IPhotoService _photoService;

    public ShopController(ApplicationDbContext context, IShopRepository shopRepository, IPhotoService photoService)
    {
        _shopRepository = shopRepository;
        _photoService = photoService;
    }
    
    public async Task<IActionResult> Index()
    {
        IEnumerable<Shop> shops = await _shopRepository.GetAll();

        return View(shops);
    }

    public async Task<IActionResult> Detail(int id)
    {
        Shop shop = await _shopRepository.GetByIdAsync(id);

        return View(shop);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateShopViewModel shopVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _photoService.AddPhotoAsync(shopVM.Image);

            var shop = new Shop
            {
                Name = shopVM.Name,
                Url = shopVM.Url,
                Image = result.Url.ToString()
            };
            _shopRepository.Add(shop);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("","Photo upload failed");
        }

        return View(shopVM);
    }
}