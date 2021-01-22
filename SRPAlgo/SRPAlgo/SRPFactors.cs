using System;
using System.Globalization;
using System.Numerics;


namespace SRPAlgo
{
    class SRPFactors
    {
        public BigInteger N { get; private set; }

        public BigInteger g { get; set; }

        public int k { get; set; }
        public SRPFactors(BigInteger g, int k)
        {
            
            N = Utilities.SimpleNumbersGenerator.GetSafetySimpleNumbers();
            Console.WriteLine("N :: " + N);
            this.g = 1000000000;
            this.k = k;
        }
    }
}
