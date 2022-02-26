using Microsoft.AspNetCore.Mvc;


namespace AuthGov.Controllers;

[ApiController]
[Route("[controller]")]
public class UpdateUserController: ControllerBase
{


    [HttpPut(Name = "UpdateUser")]
    public void Put()
    {
        /*
        //how to get the user ? 
        UpdateUser.UpdateUserDb(collection, "Matthias", "test", "test@test.com", new BsonDocument());*/
    }
    
}