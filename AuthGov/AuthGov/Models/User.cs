using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthGov.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id;
    [DataType(DataType.Password)]
    [BsonElement("password")]
    public string pwd;
    [StringLength(50)]
    [BsonElement("name")]
    public string name;
    [DataType(DataType.EmailAddress)]
    [BsonElement("email")]
    public string address;
    public string key;

    public User(string name, string address, string pwd, string key)
    {
        this.name = name;
        this.pwd = pwd;
        this.address = address;
        this.key = key;
    }
}