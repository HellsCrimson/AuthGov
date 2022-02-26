using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthGov.Controllers;

[ApiController]
[Route("[controller]")]

public class GetUserController : ControllerBase
{
    [HttpGet(Name = "GetUser")]
    public void Get()
    {
        MongoClient dbClient = new MongoClient("mongodb+srv://AuthGov:D1JSkTv7K3i0hPOO@cluster0.xb2gr.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

        var db = dbClient.GetDatabase("AuthGov");
        var collection = db.GetCollection<BsonDocument>("users");

        var filter = Builders<BsonDocument>.Filter.Empty;
        var result = collection.Find(filter).ToList();
        
        foreach (var user in result)
        {
            Console.WriteLine(user.ToJson());
        }
    }
}