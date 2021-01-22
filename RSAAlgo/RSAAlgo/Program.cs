using System;
using System.Numerics;

namespace RSAAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("User A");
            User A = new User("A");
            Console.WriteLine("User B");
            User B = new User("B");

            BigInteger messageToB = 111111;
            var encrypted_message = A.Encrypt(messageToB, B);
            var decrypted_message = B.Decrypt(encrypted_message);

            WorkDescription(A, B, messageToB, encrypted_message, decrypted_message);


            BigInteger messageToA = 22222222;
            var encrypted_message2 = B.Encrypt(messageToA, A);
            var decrypted_message2 = A.Decrypt(encrypted_message2);

            WorkDescription(B, A, messageToA, encrypted_message2, decrypted_message2);

        }

        public static void WorkDescription(User A, User B, BigInteger message, BigInteger encrypted_message, BigInteger decrypted_message)
        {
            Console.WriteLine();
            Console.WriteLine("{0} send {1} {2}", A.Name, B.Name, message);
            Console.WriteLine(nameof(encrypted_message) + " :: " + encrypted_message);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(nameof(decrypted_message) + " :: " + decrypted_message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
