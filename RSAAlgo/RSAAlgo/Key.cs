using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace RSAAlgo
{
    public class Key
    {
        public Key(BigInteger exponent, BigInteger module)
        {
            Exponent = exponent;
            Module = module;
        }

        public BigInteger Exponent { get; private set; }

        public BigInteger Module { get; private set; }
    }
}
