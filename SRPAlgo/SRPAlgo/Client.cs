using System;
using System.Numerics;

namespace SRPAlgo
{
    class Client
    {
        private string salt;
        public string UserName { get; set; }
        public SRPFactors factors { get; set; }

        private string password = "";

        private BigInteger x;
        private BigInteger verifier;
        private BigInteger u;

        public Client(string userName,string password, SRPFactors factors)
        {
            UserName = userName;
            this.factors = factors;
            this.password = password;

            Console.WriteLine("\nClient Created\n\tUserName :: {0}\n\tPassword :: {1}", userName, password);

        }

        public void Registration(Server server)
        {
            Console.WriteLine("\nRegistration\n\tClient Side");

            salt = Utilities.Strings.GetRandomString(20);
            x = Utilities.ShaHashing.Hash512((salt + password));
            verifier = BigInteger.ModPow(factors.g, x, factors.N);

            Console.WriteLine("\tsalt :: {0}\n\tx :: {1}\n\tv :: {2}", salt, x, verifier);
            Console.WriteLine("\n\tClient -> Server {I,S,V}");

            server.RegisterClient(UserName, salt, verifier);
        }
        public void Authentication(Server server)
        {
            Console.WriteLine("\nAuthentication\n\tClient Side");
            int a = new Random().Next(3, int.MaxValue/1000);
            BigInteger A = BigInteger.ModPow(factors.g, a, factors.N);

            Console.WriteLine("\ta :: {0}\n\tA :: {1}", a, A);
            Console.WriteLine("\n\tClient -> Server {I,A}");

            var salt_B = server.AuthenticateClientFirst(UserName, A);

            Console.WriteLine("\nAuthentication\n\tClient Side");

            u = Utilities.ShaHashing.Hash512(A.ToString()+ salt_B.Item2);
            if (u == 0)
            {
                throw new Exception("Connection lost");
            }
            var x = Utilities.ShaHashing.Hash512(salt + password);
            var S = BigInteger.ModPow(salt_B.Item2 - factors.k * BigInteger.ModPow(factors.g, x, factors.N), (a + u * x), factors.N);
            var K = Utilities.ShaHashing.Hash512(S.ToString());

            var M = Utilities.ShaHashing.Hash512(XOR(Utilities.ShaHashing.Hash512(factors.N.ToString()).ToByteArray(), Utilities.ShaHashing.Hash512(factors.g.ToString()).ToByteArray()) +
                Utilities.ShaHashing.Hash512(UserName) + salt + A + salt_B.Item2 + K);


            Console.WriteLine("\tu :: {0}\nu != 0\n\tx :: {1}\n\tS :: {2}", u, x, S);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tK :: {0}", K);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\tM :: {0}", M);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\tClient -> Server {M}");


            server.AuthenticateClientSecond();
            var r = server.AuthenticateClientThird(M);
            var R = Utilities.ShaHashing.Hash512(A.ToString() + M + K);
            if (R == r)
            {
                Console.WriteLine("OK");
            }else
            {
                Console.WriteLine("Client Error");
            }


            Console.WriteLine("\nAuthentication\n\tClient Side");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\tR :: {0}", R);
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("Client_R == Server_R");
            Console.WriteLine("Authentication success");

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
