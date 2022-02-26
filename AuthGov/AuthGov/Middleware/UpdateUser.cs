using MongoDB.Bson;
using MongoDB.Driver;


namespace AuthGov.Middleware;

public class UpdateUser
{
    public static void UpdateUserDb(IMongoCollection<BsonDocument> collection, string name, string password,
        string email, BsonDocument olduser)
    {
        //find how to update a specific parameter
        collection.UpdateOne(olduser, $"set");
    }
}