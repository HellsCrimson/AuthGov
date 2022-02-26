using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using AuthGov.Middleware;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthGov.Controllers;

[ApiController]
[Route("[controller]")]
public class AddUserController : ControllerBase
{
    [HttpPut(Name = "AddUser")]
    public void Put()
    {
        MongoClient dbClient = new MongoClient("mongodb+srv://AuthGov:D1JSkTv7K3i0hPOO@cluster0.xb2gr.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

        var db = dbClient.GetDatabase("AuthGov");
        var collection = db.GetCollection<BsonDocument>("users");
        
        AddUser.AddUserDb(collection, "Matthias", "test", "test@test.com");
    }
}