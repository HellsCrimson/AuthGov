using Microsoft.AspNetCore.Mvc;

namespace AuthGov.Controllers;

[ApiController]
[Route("[controller]")]

public class GetUserController : ControllerBase
{
    [HttpGet(Name = "GetUser")]
    public string Get()
    {
        User user = new User("test", "test");
        return "a";
    }
}