using System.Security.Cryptography;

namespace AuthGov.LocalAuth;
using System;
using System.IO;

enum keyFileType
{
    CONTRACT_ADDRESS = 0,
    PRIVATE_CONTRACT_KEY = 1,
    PUBLIC_CONTRACT_KEY = 2,
    ELGAMAL_PUBLIC = 3,
    ELGAMAL_PRIVATE = 4
}
public class KeyReader
{
    private DriveInfo? _secureDrive;
    private string[]? _filePaths;

    public bool Init(int password)
    {
        
        DriveInfo?[] drives = DriveInfo.GetDrives();
        
        foreach (var drive in drives)
        {
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
        int publicKey = Elgamal.GenerateKeys();
        using (StreamWriter sw = new StreamWriter(_secureDrive.RootDirectory + "/wpublicElgamalKey.key"))
        {
            sw.Write(publicKey);
        }
        using (StreamWriter sw = new StreamWriter(_secureDrive.RootDirectory + "/zprivateElgamalKey.key"))
        {
            sw.Write(password);
        }
        
        _filePaths = Directory.GetFiles(_secureDrive.RootDirectory.ToString());
        Array.Sort(_filePaths);
        foreach (string path in _filePaths)
        {
            Console.WriteLine(path);
        }
        EncryptPrivateKey(password);
        return checkKeysValidity();
    }

    /// <summary>
    /// Reads the selected key type. Asks for a password if the private key is asked
    /// </summary>
    /// <param name="keyType">The key type to read. Must be "private" or "public"</param>
    /// <returns>The corresponding login key</returns>
    public string ReadKey(string keyType, int password = 0000)
    {
        if (keyType == "private" && !CheckLogin(password))
        {
            throw new ArgumentException("Password invalid.");
        }
        string key = "";
        if (keyType != "public" && keyType != "private")
        {
            throw new ArgumentException("Not a valid key type request");
        }
        using (StreamReader sr = new StreamReader(_secureDrive.RootDirectory + "/" + keyType + "Key.agk"))
        {
            key =  sr.ReadToEnd();
        }

        if (keyType == "public")
        {
            return key;
        }
        else
        {
            return GetDecryptedKey(password);
        }
    }

    public bool checkKeysValidity()
    {
        string root = _secureDrive.RootDirectory.ToString();
        bool doFilesExist = _filePaths.Contains(root + "/publicKey.agk") && 
                            _filePaths.Contains(root + "/privateKey.agk") && 
                            _filePaths.Contains(root + "/contractAddress.agk");
        
        bool areFilesValid = File.ReadAllText(root + "/publicKey.agk").Length == 40 &&
                             File.ReadAllText(root + "/privateKey.agk").Length == 64 &&
                             File.ReadAllText(root + "/contractAddress.agk").Length == 40;
        
        return doFilesExist && areFilesValid;
    }

    public void EncryptPrivateKey(int password = 1234)
    {
        string encrypted = "";
        (int, int)[] c = Elgamal.Execute(File.ReadAllText(_filePaths[(int)keyFileType.PRIVATE_CONTRACT_KEY]), 
            Int32.Parse(File.ReadAllText(_filePaths[(int)keyFileType.ELGAMAL_PUBLIC])), password); 
        foreach ((int, int) valueTuple in c)
        {
            encrypted += valueTuple.Item1 + " " + valueTuple.Item2 + " ";
        }

        using (StreamWriter sw = new StreamWriter(_filePaths[(int)keyFileType.PRIVATE_CONTRACT_KEY]))
        {
            sw.Write(encrypted);
        }
    }
    private string GetDecryptedKey(int password)
    {
        int toPut = 0;
        int indice = 0;
        int switcher = 0;
        (int, int) toPutCouple = (0, 0);
        (int, int)[] message = new (int, int)[64];
        string encryptedKey = "";
        using (StreamReader sr = new StreamReader(_filePaths[(int)keyFileType.PRIVATE_CONTRACT_KEY]))
        {
            encryptedKey = sr.ReadToEnd();
        }

        foreach (char c in encryptedKey)
        {
            if (c != ' ')
            {
                toPut = toPut * 10 + c - 48;
            }
            else
            {
                if (switcher == 0)
                {
                    toPutCouple.Item1 = toPut;
                    switcher++;
                }
                else
                {
                    toPutCouple.Item2 = toPut;
                    
                    message[indice] = toPutCouple;
                    toPutCouple = (0, 0);
                    switcher = 0;
                    indice++;
                }
            }
        }
        
        return Elgamal.Decripter(message, Int32.Parse(File.ReadAllText(_filePaths[(int)keyFileType.ELGAMAL_PUBLIC])),password);
    }
    private bool CheckLogin(int pass)
    {
        string test = File.ReadAllText(_filePaths[(int) keyFileType.ELGAMAL_PRIVATE]);
        string test2 = pass.ToString();
        Console.WriteLine(test + " " + test2);
        return true;
    }
}