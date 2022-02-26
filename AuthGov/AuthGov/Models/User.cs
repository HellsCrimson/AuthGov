using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthGov.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    private int ID;
    [DataType(DataType.Password)]
    private string pwd;
    [StringLength(50)]
    private string name;
    [DataType(DataType.EmailAddress)]
    private string address;
    private File[] _files;

    public User(string name, string address, string pwd)
    {
        this.name = name;
        this.pwd = pwd;
        this.address = address;
    }

    private int getId()
    {
        return ID;
    }

    private void setId(int id)
    {
        this.ID = id;
    }

    private void setName(string name)
    {
        this.name = name;
    }




}