using System.Security.Cryptography;

namespace AuthGov.LocalAuth;
using System;
using System.IO;

public class KeyReader
{
    private DriveInfo? _secureDrive;
    private string[]? _filePaths;

    public bool Init()
    {
        
        DriveInfo?[] drives = DriveInfo.GetDrives();
        
        foreach (var drive in drives)
        {
            Console.WriteLine(drive.Name);
            Console.WriteLine(drive.VolumeLabel);
            if (drive.VolumeLabel.Contains("TESTDRIVE"))
            {
                _secureDrive = drive;
            }
        }

        if (_secureDrive == null)
        {
            Console.WriteLine("No authorised drive was detected, are you sure your authentification USB device is plugged in ?");
            return false;
        }
        Console.WriteLine("The correct key is plugged in !");
        _filePaths = Directory.GetFiles(_secureDrive.RootDirectory.ToString());
        if (_filePaths.Length == 0)
        {
            CreateKeys();
        }
        return true;
    }

    private void CreateKeys()
    {
    }

    /// <summary>
    /// Reads the selected key type. Asks for a password if the private key is asked
    /// </summary>
    /// <param name="keyType">The key type to read. Must be "private" or "public"</param>
    /// <returns>The corresponding login key</returns>
    public string ReadKey(string keyType)
    {
        string key = "";
        if (keyType != "public" && keyType != "private")
        {
            throw new ArgumentException("Not a valid key type request");
        }
        using (StreamReader sr = new StreamReader(_secureDrive.RootDirectory + keyType + "Key.agk"))
        {
            key =  sr.ReadToEnd();
        }

        if (keyType == "public")
        {
            return key;
        }
        else
        {
            return GetDecryptedKey();
        }
    }

    private string GetDecryptedKey()
    {
        //TODO
        return "";
    }
    private bool CheckLogin()
    {
        //TODO
        return false;
    }
}