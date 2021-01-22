using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace DiffieHellmanAlgo
{
    public class User : IUser
    {
        public string Name { get; set; }
        public BigInteger PublicBase { get; private set; }
        public BigInteger PublicModule { get; private set; }
   
        private BigInteger _privateKey;

        private BigInteger SecretKey;
        public BigInteger OpenKey { get; private set; }

        public BigInteger FriendKey { get; set; }

        public User(string name, BigInteger privateKey)
        {
            _privateKey = privateKey;
            Name = name;
        }
        public void GenerateBaseAndModule()
        {
            PublicBase = SimpleNumbersGenerator.GetRandomSimpleNumber();
            PublicModule = SimpleNumbersGenerator.PrimitiveRoot.GetPRoot(PublicBase);
            Console.WriteLine();
            Console.WriteLine("{0} :: Public Base = {1}\n{0} :: Public Module = {2}", Name, PublicBase, PublicModule);
        }

        public void GetBaseAndModule(BigInteger pb, BigInteger pm)
        {
            PublicBase = pb;
            PublicModule = pm;
            Console.WriteLine();
            Console.WriteLine("{0} :: Public Base = {1}\n{0} :: Public Module = {2}", Name, PublicBase, PublicModule);
        }

        public void GenerateOpenKey()
        {
            OpenKey = BigInteger.ModPow(PublicModule, _privateKey, PublicBase);
            Console.WriteLine();
            Console.Write("{0} :: OpenKey = ", Name);
            Console.WriteLine(OpenKey);
        }


        public void CalculateSecretKey()
        {
            SecretKey = BigInteger.ModPow(FriendKey, _privateKey, PublicBase);
            Console.WriteLine();
            Console.Write("{0} :: Test :: SecretKey = ", Name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(SecretKey);
            Console.ForegroundColor = ConsoleColor.White;
        }


    }
}
