namespace AuthGov;

public class File
{
    private string encode;
    private string description;
    private string title;
    private long id;

    public File(string title, string description)
    {
        this.title = title;
        this.description = description;
        id = 0;
        encode = "";
    }

}