namespace AuthGov.LocalAuth;
using System;
using System.IO;

public class KeyReader
{
    private StringReader sr;
    private DriveInfo[] drives;
    private DriveInfo drive;

    public KeyReader()
    {
        drives = DriveInfo.GetDrives();
        
        foreach (var _drive in drives)
        {
            Console.WriteLine(_drive.VolumeLabel);
        }
    }
}