using System.Numerics;

namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public class RSAKeyClass
    {
        public System.Numerics.BigInteger Key;
        public System.Numerics.BigInteger N;

        public RSAKeyClass(System.Numerics.BigInteger Key, System.Numerics.BigInteger N)
        {
            this.Key = Key;
            this.N = N;
        }
    }
}
