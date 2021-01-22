using System;

namespace SRPAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            var factors = new SRPFactors(100000000, 3);
            var server = new Server(factors);
            var client = new Client("Khan", "12345qwe", factors);

            client.Registration(server);
            client.Authentication(server);
        }
    }
}
