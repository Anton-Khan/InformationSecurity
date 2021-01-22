using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace RSAAlgo
{
    public static class Algoritm
    {
        public static (Key, Key) Generate()
        {
            var p = SimpleNumbersGenerator.GetRandomSimpleNumber();
            var q = SimpleNumbersGenerator.GetRandomSimpleNumber();
            var n = p * q;
            var EilerF = (p - 1) * (q - 1);
            var e = Calculate_E(EilerF);
            var d = Calculate_D(e, EilerF);
            Console.WriteLine("\t"+nameof(p) + " :: " + p);
            Console.WriteLine("\t" + nameof(q) + " :: " + q);
            Console.WriteLine("\t" + nameof(n) + " :: " + n);
            Console.WriteLine("\t" + nameof(EilerF) + " :: " + EilerF);
            Console.WriteLine("\t" + nameof(e) + " :: " + e);
            Console.WriteLine("\t" + nameof(d) + " :: " + d);

            return (new Key(e, n), new Key(d, n));
        }


        public static BigInteger Encrypt(BigInteger message, Key publicKey)
        {
            return BigInteger.ModPow(message, publicKey.Exponent, publicKey.Module);
        }

        public static BigInteger Decrypt(BigInteger message, Key privateKey)
        {
            return BigInteger.ModPow(message, privateKey.Exponent, privateKey.Module);
        }

        private static BigInteger Calculate_E(BigInteger n)
        {
            for (BigInteger i = 3; i < n; i+=2)
            {
                if (IsCoprime(i,n))
                {
                    return i;
                }
            }
            Console.WriteLine("Can't find E (Coprime numbers error)");
            throw new Exception("Can't find E (Coprime numbers error)");
        }

        private static BigInteger Calculate_D(BigInteger e, BigInteger n)
        {
            BigInteger d=1;
            BigInteger y=1;
            BigInteger g = gcd(e, n, ref d, ref y);
            if (g != 1)
            {
                Console.WriteLine("No Solution");
                throw new Exception("Can't find D ");
            }
            else
            {
                d = (d % n + n) % n;
                return d;
            }
            
        }
        private static BigInteger gcd(BigInteger a, BigInteger b, ref BigInteger x, ref BigInteger y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            BigInteger x1=0, y1=0;
            BigInteger d = gcd(b % a, a, ref x1, ref y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        public static bool IsCoprime(BigInteger num1, BigInteger num2)
        {
            BigInteger min = num1;
            if (num2 < num1)
                min = num2;

            for (BigInteger i = 3; i <= min; i++)
            {
                if (num1 % i == 0 && num2 % i == 0)
                    return false;
            }

            return true;
        }
    }
}
