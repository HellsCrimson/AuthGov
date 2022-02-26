using System;

namespace safeLock
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(pow_mod(5,173,374));
            int c1 = 0, c2 = 0;
            int publicKey = choosePrime(); //need to chance -> get a different public key for every one
            Random random = new Random();
            
            int secretKey = random.Next(publicKey);
            int p = pow_mod(3,secretKey, publicKey);
            Console.WriteLine(publicKey);
            elgamal_en(78,p,publicKey,3,ref c1,ref c2);
            Console.WriteLine(elgamal_de(c1,c2,secretKey,publicKey,3));
        }
        private static int pow_mod(int a, int pui, int mod)
        {
            int tmp = 1;
            for(int counter = 0; counter < pui; counter++)
            {
                
                tmp = tmp * a;
                tmp %= mod;
            }

            return tmp;
        }

        private static void elgamal_en(int m, int pub, int p, int g, ref int c1, ref int c2)
        {
            Random r = new Random();
            int k = r.Next(p - 1);
            c1 = pow_mod(g, k, p);
            c2 = m * pow_mod(pub, k, p) % p;
        }

        private static int elgamal_de(int c1, int c2, int pri, int p, int g)
        {
            int tmp = pow_mod(c1, p - 2, p);
            int m = c2 * pow_mod(tmp, pri, p) % p;
            return m;
        }
        
        private static int choosePrime()
        {
            int p = 0;
            do
            {
                Random r = new Random();
                p = r.Next(250, 1500);
            } while (!primeNumber(p) || !isprimeMultiple(p - 1));
            Console.WriteLine(p);
            return p;
        }
        private static bool primeNumber(long n) //test if n is a prime number, we suppose n > 1
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

        private static int gcd(int a, int b)
        {
            if (a == 0)
                return b;

            if (b == 0)
                return a;
            if (a % 2 == 0 && b % 2 == 0)
                return gcd(a >> 1, b >> 1) << 1;
            if (b % 2 == 0)
                return gcd(a, b >> 1);
            if (a % 2 == 0)
                return gcd(a >> 1, b);
            return gcd(abs(a - b), min(a, b));

        }

        private static int abs(int n)
        {
            if (n > 0)
                return n;
            return -1 * n;
        }

        private static int min(int a, int b)
        {
            if (a > b)
                return b;
            return a;
        }

        private static bool isprimeMultiple(int n)
        {
            int counter = 2;
            while (counter * counter < n)
            {
                if (n % counter == 0 && primeNumber(counter) && primeNumber(n % counter))
                    return true;

            }

            return false;
        }
    }
}
