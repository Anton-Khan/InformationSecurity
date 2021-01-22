using System;
using System.Numerics;

namespace RSAAlgo
{
    class User
    {
        public User(string name)
        {
            Name = name;
            var keys = Algoritm.Generate();
            PublicKey = keys.Item1;
            PrivateKey = keys.Item2;
        }

        public string Name { get; set; }
        public Key PublicKey { get; private set; }
        private Key PrivateKey { get; set; }

        public BigInteger Encrypt(BigInteger message, User friend)
        {
            return Algoritm.Encrypt(message, friend.PublicKey);
        }
        public BigInteger Decrypt(BigInteger enc_message)
        {
            return Algoritm.Decrypt(enc_message, PrivateKey);
        }
    }
}
