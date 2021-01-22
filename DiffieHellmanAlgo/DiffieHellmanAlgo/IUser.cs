using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DiffieHellmanAlgo
{
    interface IUser
    {
        public void GenerateBaseAndModule();
        public void GetBaseAndModule(BigInteger pb, BigInteger pm);
        public void GenerateOpenKey();
        public void CalculateSecretKey();

    }
}
