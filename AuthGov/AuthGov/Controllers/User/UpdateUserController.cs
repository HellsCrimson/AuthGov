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
public class UpdateUserController: ControllerBase
{


    [HttpPut(Name = "UpdateUser")]
    public void Put()
    {
        MongoClient dbClient = new MongoClient("mongodb+srv://AuthGov:D1JSkTv7K3i0hPOO@cluster0.xb2gr.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

        var db = dbClient.GetDatabase("AuthGov");
        var collection = db.GetCollection<BsonDocument>("users");
        //how to get the user ? 
        UpdateUser.UpdateUserDb(collection, "Matthias", "test", "test@test.com", new BsonDocument());
    }
    
}