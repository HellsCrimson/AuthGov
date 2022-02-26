using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
namespace AuthGov.Controllers;

[ApiController]
[Route("[controller]")]
public class AddUserController : ControllerBase
{
    [HttpGet(Name = "AddUser")]
    public string Add()
    {
        User user = new User("test", "test", "test");
        
        return "a";
    }
}