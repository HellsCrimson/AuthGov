using AuthGov.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthGov.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GetUserController : ControllerBase
{
    private readonly UserService _userService;
    
    public GetUserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet(Name = "GetUser")]
    public async void Get(string id)
    {
        await _userService.GetAsync(id);
    }
}