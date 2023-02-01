using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using OrdersManager.Data.Enum;

namespace OrdersManager.Models;

public class AppUser: IdentityUser
{
    public string? ProfileImageUrl { get; set; }
}