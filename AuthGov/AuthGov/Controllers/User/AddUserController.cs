using Microsoft.AspNetCore.Mvc;

namespace AuthGov.Controllers;

[ApiController]
[Route("[controller]")]
public class AddUserController : ControllerBase
{
    [HttpGet(Name = "AddUser")]
    public string Add()
    {
        User user = new User("test", "test");
        return "a";
    }
}