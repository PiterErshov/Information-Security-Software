using System.Numerics;

namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public class RSAKeyClass
    {
        public BigInteger Key;
        public BigInteger N;

        public RSAKeyClass(BigInteger Key, BigInteger N)
        {
            this.Key = Key;
            this.N = N;
        }

        public override string ToString()
        {
            return "Key: " + Key.ToString() + ", N: " + N.ToString();
        }

        public string ToString(string format)
        {
            return "Key: " + Key.ToString(format) + ", N: " + N.ToString(format);
        }
    }
}
