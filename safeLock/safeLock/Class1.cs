using System;
using System.ComponentModel;

namespace safeLock
{
    public class Class1
    {
        private int PublicKey;
        private int secretKey;

        public void Main(string[] args)
        {
            Console.WriteLine(primeNumber(30031));
        }

        private int pow_mod(int a, int b, int p)
        {
            int ans = 1;
            int tmp = a % p;
            while (b != 0)
            {
                if (b % 2 != 0)
                    ans = ans * tmp % p;
                b = b >> 1;
                tmp = tmp * tmp * p;

            }

            return ans % p;
        }

        private void elgamal_en(int m, int pub, int p, int g, ref int c1, ref int c2)
        {
            int k = 5;
            c1 = pow_mod(c1, p - 2, p);
            c2 = m * pow_mod(pub, k, p) % p;
        }

        private int elgamal_de(int c1, int c2, int pri, int p, int g)
        {
            int tmp = pow_mod(c1, p - 2, p);
            int m = c2 * pow_mod(tmp, pri, p) % p;
            return m;
        }
        
        private int choosePrime()
        {
            int p = 0;
            do
            {
                Random r = new Random();
                p = r.Next(100, 10000000);
            } while (!primeNumber(p) && !isprimeMultiple(p - 1));

            return p;
        }
        private bool primeNumber(long n) //test if n is a prime number, we suppose n > 1
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

        private int gcd(int a, int b)
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

        private int abs(int n)
        {
            if (n > 0)
                return n;
            return -1 * n;
        }

        private int min(int a, int b)
        {
            if (a > b)
                return b;
            return a;
        }

        private bool isprimeMultiple(int n)
        {
            int counter = 2;
            while (counter * counter < n)
            {
                if (n % counter * n % counter == n && primeNumber(counter) && primeNumber(n % counter))
                    return true;

            }

            return false;
        }
    }
}