using AuthGov.Models;
using AuthGov.Services;
using Microsoft.AspNetCore.Mvc;


namespace AuthGov.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UpdateUserController: ControllerBase
{
    private readonly UserService _userService;
    
    public UpdateUserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPut(Name = "UpdateUser")]
    public async void Put(string id, string address, string email)
    {
        var oldUser = _userService.GetAsync(id).Result;
        if (oldUser == null)
            return;
        await _userService.UpdateAsync(id, new User(oldUser.Id, oldUser.name, address, oldUser.pwd, oldUser.key, email));
    }
    
}