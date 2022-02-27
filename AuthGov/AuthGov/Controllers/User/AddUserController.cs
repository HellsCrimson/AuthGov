using Microsoft.AspNetCore.Mvc;
using AuthGov.Models;
using AuthGov.Services;
using System.Security.Cryptography;
using System.Text;
using AuthGov.LocalAuth;

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
    public async void Post(string name, string address, int pwd)
    {
        KeyReader keyReader = new KeyReader();
        if (keyReader.Init(pwd))
            return;
        string pwdHashed = GetHashString(pwd.ToString());
        string key = keyReader.ReadKey("public");
        await _userService.CreateAsync(new User(name, address, pwdHashed, key));
    }
    
    public static byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }
}