using System.Dynamic;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthGov;

public class User
{
    private int ID;
    private string pwd;
    private string name;
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
    
    
    
    
}