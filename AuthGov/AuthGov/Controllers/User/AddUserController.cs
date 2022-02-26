using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using AuthGov.Config;
using AuthGov.Middleware;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthGov.Controllers;

[ApiController]
[Route("[controller]")]
public class AddUserController : ControllerBase
{
    [HttpPost(Name = "AddUser")]
    public void Post()
    {
        MongoClient dbClient = new MongoClient(DbUrl.url);

        var db = dbClient.GetDatabase("AuthGov");
        var collection = db.GetCollection<BsonDocument>("users");
        AddUser.AddUserDb(collection, "Matthias", "test", "test@test.com");
    }
}