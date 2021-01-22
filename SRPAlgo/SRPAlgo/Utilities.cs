using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace SRPAlgo
{
    public static class Utilities
    {
        static Random random = new Random();

        public static class SimpleNumbersGenerator
        {
            public static BigInteger GetRandomSimpleNumber()
            {

                while (true)
                {
                    var randomNumber = random.Next(int.MaxValue / 100, int.MaxValue / 10).ToString();
                    var number = BigInteger.Parse(randomNumber);

                    if (MillerR_test(number, 10))
                    {
                        return number;
                    }
                }
            }

            public static BigInteger GetSafetySimpleNumbers()
            {

                while (true)
                {
                    var randomNumber = random.Next(int.MaxValue / 100, int.MaxValue / 10).ToString();
                    var number = BigInteger.Parse(randomNumber);
                    var q = (number - 1) / 2;
                    if (MillerR_test(number, 60) && MillerR_test(q, 60))
                    {
                        return number;
                    }
                }
            }

            private static bool MillerR_test(BigInteger n, int k)
            {

                if (n == 2 || n == 3)
                    return true;

                if (n < 2 || n % 2 == 0)
                    return false;


                BigInteger t = n - 1;
                int s = 0;

                while (t % 2 == 0)
                {
                    t /= 2;
                    s++;
                }

                for (int i = 0; i < k; i++)
                {
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                    var _a = new byte[n.ToByteArray().LongLength];
                    BigInteger a;

                    do
                    {
                        rng.GetBytes(_a);
                        a = new BigInteger(_a);
                    }
                    while (a < 2 || a >= n - 2);

                    BigInteger x = BigInteger.ModPow(a, t, n);

                    if (x == 1 || x == n - 1)
                        continue;

                    for (int r = 1; r < s; r++)
                    {
                        x = BigInteger.ModPow(x, 2, n);

                        if (x == 1)
                            return false;

                        if (x == n - 1)
                            break;
                    }

                    if (x != n - 1)
                        return false;
                }

                return true;
            }
        }
        public static class ShaHashing
        {
            public static BigInteger Hash512(string inputString)
            {
                SHA512 sha512 = SHA512Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha512.ComputeHash(bytes);
                return BigInteger.Abs(new BigInteger(hash));
            }
        }

        public static class PrimitiveRoot
        {

            public static BigInteger GetPRoot(BigInteger p)
            {
                for (BigInteger i = 0; i < p; i++)
                    if (IsPRoot(p, i))
                        return i;
                Console.WriteLine("Can't calculate Primitive Root");
                throw new Exception("Can't calculate Primitive Root");
            }

            static bool IsPRoot(BigInteger p, BigInteger a)
            {
                if (a == 0 || a == 1)
                    return false;
                BigInteger last = 1;
                var set = new HashSet<BigInteger>();
                for (BigInteger i = 0; i < p - 1; i++)
                {
                    last = (last * a) % p;
                    if (set.Contains(last)) // Если повтор
                        return false;
                    set.Add(last);
                }
                return true;
            }
        }

        public static class Strings
        {
            public static string GetRandomString(int length)
            {
                Random random = new Random();
                char[] az = Enumerable.Range('a', 'z' - 'a' + 1)
                    .Select(i => (Char)i)
                    .ToArray()
                    .Concat(Enumerable.Range('A', 'Z' - 'A' + 1)
                    .Select(i => (Char)i)
                    .ToArray())
                    .ToArray();
                string result = "";
                for (int i = 0; i < length; i++)
                    result += az[random.Next(0, az.Count())];
                
                return result;
            }
        }
    }
}
