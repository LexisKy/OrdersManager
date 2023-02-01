using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.Data.Enum;
using OrdersManager.Interfaces;
using OrdersManager.Models;
using OrdersManager.ViewModels;

namespace OrdersManager.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IPhotoService _photoService;
    private readonly UserManager<AppUser> _userManager;

    public UserController(IUserRepository userRepository,  IPhotoService photoService, UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _photoService = photoService;
        _userManager = userManager;
    }
    
    private void MapUserEdit(AppUser user, EditUserViewModel editVM, ImageUploadResult photoResult)
    {
        user.Id = editVM.Id;
        if (photoResult != null)
        {
            user.ProfileImageUrl = photoResult.Url.ToString();
        }

        if (editVM.Password != null)
        {
            var passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<AppUser>)) as IPasswordHasher<AppUser>;
            user.PasswordHash = passwordHasher.HashPassword(user, editVM.Password);
        }
        user.Email = editVM.Email;
        user.NormalizedEmail = editVM.Email.ToUpper();
    }
    
    [Authorize(Roles="admin")]
    [HttpGet("users")]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllUsers();
        List<UserViewModel> result = new List<UserViewModel>();
        foreach (var user in users)
        {
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Image = user.ProfileImageUrl,
                Role = userRoles.First()
            };
            result.Add(userViewModel);
        }
        return View(result);
    }

    public async Task<IActionResult> Detail(string id)
    {
        var user = await _userRepository.GetUserById(id);
        var userDetailViewModel = new UserDetailViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            UserEmail = user.Email,
            Image = user.ProfileImageUrl
        };
        return View(userDetailViewModel);
    }
    
    public async Task<IActionResult> EditUserFromAdmin(string id)
    {
        var curUserId = id;
        var user = await _userRepository.GetUserById(curUserId);
        if (user == null)
        {
            return View("Error");
        }

        var editUserViewModel = new EditUserViewModel()
        {
            Id = curUserId,
            ProfileImageUrl = user.ProfileImageUrl,
            Email = user.Email,
            Password = user.PasswordHash
        };
        return View(editUserViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditUserFromAdmin(EditUserViewModel editUserViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit profile");
            return View("EditUserFromAdmin", editUserViewModel);
        }

        var user = await _userRepository.GetByIdNoTracking(editUserViewModel.Id);
        if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
        {
            var photoResult = await _photoService.AddPhotoAsync(editUserViewModel.Image);
            MapUserEdit(user, editUserViewModel, photoResult);

            _userRepository.Update(user);
            return RedirectToAction("EditUserFromAdmin");
        }
        else
        {
            try
            {
                if (editUserViewModel.Image != null)
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Could not delete photo");
                return View(editUserViewModel);
            }
            if (editUserViewModel.Image != null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editUserViewModel.Image);
                MapUserEdit(user, editUserViewModel, photoResult);
            }
            else
            {
                ImageUploadResult photoResult = null;
                MapUserEdit(user, editUserViewModel, photoResult);
            }

            _userRepository.Update(user);
            return RedirectToAction("EditUserFromAdmin");
        }
    }

    [Authorize(Roles="admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [Authorize(Roles="admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel userVM)
    {
        if (!ModelState.IsValid)
        {
            return View(userVM);
        }
        AppUser user = new AppUser()
        {
            Email = userVM.Email,
            UserName = userVM.UserName
        };

        var result = await _userManager.CreateAsync(user, userVM.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(userVM);
    }
    public async Task<ActionResult> LockOut(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        if (user != null)
        { 
           await _userManager.RemoveFromRoleAsync(user, UserRoles.User);
           await _userManager.AddToRoleAsync(user, UserRoles.Block);
        }

        return RedirectToAction("Index");
    }
    public async Task<ActionResult> UnLockOut(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            await _userManager.RemoveFromRoleAsync(user, UserRoles.Block);
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        return RedirectToAction("Index");
    }
}