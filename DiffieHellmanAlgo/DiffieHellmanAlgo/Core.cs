using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace DiffieHellmanAlgo
{
    public static class Core
    {
        public static void Connect(User you, User friend)
        {
            you.GenerateBaseAndModule();
            friend.GetBaseAndModule(you.PublicBase, you.PublicModule);
            you.GenerateOpenKey();
            friend.GenerateOpenKey();
            you.FriendKey = friend.OpenKey;
            friend.FriendKey = you.OpenKey;
            you.CalculateSecretKey();
            friend.CalculateSecretKey();
        }


    }

    
}
