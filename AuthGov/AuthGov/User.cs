namespace AuthGov;

public class User
{
    private int ID;
    private string description;
    private string name;
    private string address;
    private File[] _files;

    public User(string name, string address)
    {
        this.name = name;
        this.address = address;
    }
}