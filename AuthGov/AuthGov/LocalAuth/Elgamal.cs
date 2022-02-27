using System;

namespace AuthGov.LocalAuth;
class Elgamal
{
    private static int secretKey;
    private static int publicKey;
    public static (int,int)[] Execute(string args)
    {

        GenerateKeys();
        secretKey = 9999;
        (int,int)[] c = Encripter(args);
        Console.WriteLine(Decripter(c));
        return c;

    }

    public static String Decripter((int,int)[] c)
    {
        String msg = "";
        for(int counter = 0; counter < 64; counter++)
        {
            int c1,c2;
            (c1,c2) = c[counter];
            msg += (char)ElgamalDe(c1,c2,secretKey,publicKey,3);
        }
        return msg;
    }

    public static (int,int)[] Encripter(String msg)
    {
        int c1 = 0, c2 = 0;
        int p = PowMod(3,secretKey, publicKey);
        (int,int)[] msgAfter = new (int,int)[64];
        for(int counter = 0; counter < 64; counter++)
        {
            ElgamalEn((int)msg[counter],p,publicKey,3,ref c1,ref c2);
            msgAfter[counter] = (c1,c2);
        }
        return msgAfter;
    }
    private static void GenerateKeys()
    {
        //int c1 = 0, c2 = 0;
        publicKey = ChoosePrime(); //need to chance -> get a different public key for every one
        //Random random = new Random();
        
        //secretKey = random.Next(publicKey);
        //int p = PowMod(3,secretKey, publicKey);
        //ElgamalEn(78,p,publicKey,3,ref c1,ref c2);
        //Console.WriteLine(ElgamalDe(c1,c2,secretKey,publicKey,3));
    }
    private static int PowMod(int a, int pui, int mod)
    {
        int tmp = 1;
        for(int counter = 0; counter < pui; counter++)
        {
            
            tmp = tmp * a;
            tmp %= mod;
        }

        return tmp;
    }

    private static void ElgamalEn(int m, int pub, int p, int g, ref int c1, ref int c2)
    {
        Random r = new Random();
        int k = r.Next(p - 1);
        c1 = PowMod(g, k, p);
        c2 = m * PowMod(pub, k, p) % p;
    }

    private static int ElgamalDe(int c1, int c2, int pri, int p, int g)
    {
        int tmp = PowMod(c1, p - 2, p);
        int m = c2 * PowMod(tmp, pri, p) % p;
        return m;
    }
    
    private static int ChoosePrime()
    {
        int p = 0;
        do
        {
            Random r = new Random();
            p = r.Next(10000,48953);//6500
        } while (!PrimeNumber(p) || !IsprimeMultiple(p - 1));
        return p;
    }
    private static bool PrimeNumber(long n) //test if n is a prime number, we suppose n > 1
    {
        if (n < 3)
            return true;
        if (n % 2 == 0 || n % 3 == 0)
            return false;
        int counter = 5;
        while (counter * counter < n)
        {
            if (n % counter == 0 || n % (counter + 2) == 0)
                return false;
            counter+=6;
        }

        return true;
    }

    private static int Gcd(int a, int b)
    {
        if (a == 0)
            return b;

        if (b == 0)
            return a;
        if (a % 2 == 0 && b % 2 == 0)
            return Gcd(a >> 1, b >> 1) << 1;
        if (b % 2 == 0)
            return Gcd(a, b >> 1);
        if (a % 2 == 0)
            return Gcd(a >> 1, b);
        return Gcd(Abs(a - b), Min(a, b));

    }

    private static int Abs(int n)
    {
        if (n > 0)
            return n;
        return -1 * n;
    }

    private static int Min(int a, int b)
    {
        if (a > b)
            return b;
        return a;
    }

    private static bool IsprimeMultiple(int n)
    {
        int counter = 2;
        while (counter * counter < n)
        {
            if (n % counter == 0 && PrimeNumber(counter) && PrimeNumber(n % counter))
                return true;

        }

        return false;
    }
}
