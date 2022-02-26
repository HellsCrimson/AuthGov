using AuthGov.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthGov.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeleteUserController : ControllerBase
{
    private readonly UserService _userService;
    
    public DeleteUserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost(Name = "RemoveUser")]
    public async void Post(string id)
    {
        await _userService.RemoveAsync(id);
    }
}