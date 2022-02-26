using Microsoft.AspNetCore.Mvc;
using AuthGov.Models;
using AuthGov.Services;

namespace AuthGov.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddUserController : ControllerBase
{
    private readonly UserService _userService;
    
    public AddUserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost(Name = "AddUser")]
    public async void Post()
    {
        await _userService.CreateAsync(new User("test", "test", "test"));
    }
}