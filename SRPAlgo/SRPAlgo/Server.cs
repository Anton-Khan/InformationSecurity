using System;
using System.Numerics;


namespace SRPAlgo
{
    class Server
    {
        private string I;
        private string s;
        private BigInteger u;
        private BigInteger v;
        private BigInteger B;
        private BigInteger K;
        private int b;
        private BigInteger A;
        private SRPFactors factors;

        public Server(SRPFactors factors)
        {
            this.factors = factors;
            Console.WriteLine("\nServer Created");
        }

        public void RegisterClient(string I, string s, BigInteger v)
        {
            this.I = I;
            this.s = s;
            this.v = v;
        }


        public (string, BigInteger) AuthenticateClientFirst(string I, BigInteger a)
        {
            if (a != 0)
            {
                Console.WriteLine("\nAuthentication\n\tServer Side");
                Console.WriteLine("A != 0");
                A = a;
                b = new Random().Next(3, int.MaxValue / 1000);
                B = ((factors.k * v) + BigInteger.ModPow(factors.g, b, factors.N)) % factors.N;
                Console.WriteLine("\tb :: {0}\n\tB :: {1}", b, B);
                Console.WriteLine("\n\tServer -> Client {s,B}");
                return (s, B);
            }
            throw new Exception("A == 0");
        }
        public void AuthenticateClientSecond()
        {
            u = Utilities.ShaHashing.Hash512(A.ToString()+B);
            if (u == 0)
            {
                throw new Exception("Connection lost");
            }

            var S = BigInteger.ModPow(A * BigInteger.ModPow(v, u, factors.N), b, factors.N);
            K = Utilities.ShaHashing.Hash512(S.ToString());
            Console.WriteLine("\nAuthentication\n\tServer Side");
            Console.WriteLine("u != 0");
            Console.WriteLine("\tS :: {0}", S);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tK :: {0}", K);
            Console.ForegroundColor = ConsoleColor.White;

        }

        public BigInteger AuthenticateClientThird(BigInteger m)
        {
            var M = Utilities.ShaHashing.Hash512(XOR(Utilities.ShaHashing.Hash512(factors.N.ToString()).ToByteArray(), Utilities.ShaHashing.Hash512(factors.g.ToString()).ToByteArray()) +
               Utilities.ShaHashing.Hash512(I) + s + A + B + K);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\tM :: {0}", M);
            Console.ForegroundColor = ConsoleColor.White;
            if (M == m)
            {
                Console.WriteLine("Server_M == Client_M");
                var R = Utilities.ShaHashing.Hash512(A.ToString() + M + K);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\tR :: {0}", R);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\n\tServer -> Client {R}");
                return R;
            }
            else
            {
                throw new Exception("M != M");
            }
            
        }
        private string XOR(byte[] key, byte[] PAN)
        {
            if (key.Length == PAN.Length)
            {
                byte[] result = new byte[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    result[i] = (byte)(key[i] ^ PAN[i]);
                }

                string hex = BitConverter.ToString(result).Replace("-", "");
                return hex;
            }

            throw new ArgumentException("Lengths are different");
        }



    }
}
