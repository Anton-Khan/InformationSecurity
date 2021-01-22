using System;

namespace DiffieHellmanAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            

            User A = new User("You", 2);
            User B = new User("Friend", 5);

            Core.Connect(A, B);

            
        }
    }
}
