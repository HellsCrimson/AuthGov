using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthGov.Middleware;

public static class AddUser
{
    public static void AddUserDb(IMongoCollection<BsonDocument> collection, string name, string password, string email)
    {
        var user = new BsonDocument
        {
            {"name", name},
            {"mdp", password},
            {"email", email}
        };
        collection.InsertOne(user);
    }
}