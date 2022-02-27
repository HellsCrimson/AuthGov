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
    [DataType(DataType.Text)]
    [BsonElement("address")]
    public string address;
    [DataType(DataType.EmailAddress)]
    [BsonElement("email")]
    public string email;
    public string key;

    public User(string name, string address, string pwd, string key, string email)
    {
        this.name = name;
        this.pwd = pwd;
        this.address = address;
        this.key = key;
        this.email = email;
    }
    
    public User(string id, string name, string address, string pwd, string key, string email)
    {
        this.Id = id;
        this.name = name;
        this.pwd = pwd;
        this.address = address;
        this.key = key;
        this.email = email;
    }
}