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
    public async void Put(string id, User updatedUser)
    {
        await _userService.UpdateAsync(id, updatedUser);
    }
    
}