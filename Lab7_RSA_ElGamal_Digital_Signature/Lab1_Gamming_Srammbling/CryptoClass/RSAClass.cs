using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public static class RSAClass
    {
        public static System.Numerics.BigInteger RandomBigIntInRange(System.Numerics.BigInteger min, System.Numerics.BigInteger max)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            if (min > max)
            {
                System.Numerics.BigInteger temp = min;
                min = max;
                max = temp;
            }

            System.Numerics.BigInteger offset = -min;
            min = 0;
            max += offset;

            System.Numerics.BigInteger value = RandomBigIntFromZero(rng, max) - offset;
            return value;
        }

        public static System.Numerics.BigInteger RandomBigIntFromZero(RandomNumberGenerator rng, System.Numerics.BigInteger max)
        {
            System.Numerics.BigInteger value;
            byte[] bytes = max.ToByteArray();
            byte ZeroBitsMask = 0b00000000;
            byte MostSignificantByte = bytes[bytes.Length - 1];

            for (int i = 7; i >= 0; i--)
            {
                if ((MostSignificantByte & (0b1 << i)) != 0)
                {
                    int ZeroBits = 7 - i;
                    ZeroBitsMask = (byte)(0b11111111 >> ZeroBits);
                    break;
                }
            }

            do
            {
                rng.GetBytes(bytes);                
                bytes[bytes.Length - 1] &= ZeroBitsMask;
                value = new System.Numerics.BigInteger(bytes);

            }
            while (value > max);

            return value;
        }

        public static bool MillerRabinTest(System.Numerics.BigInteger N, System.Numerics.BigInteger D)
        {
            System.Numerics.BigInteger a = RandomBigIntInRange(2, N - 2);

            System.Numerics.BigInteger x = System.Numerics.BigInteger.ModPow(a, D, N);

            if (x == 1 || x == N - 1)
                return true;
            else
                return false;
        }

        public static bool IsPrime(System.Numerics.BigInteger N)
        {
            if (N < 2)
                return false;

            if (N == 2 || N == 3)
                return true;

            if (N % 2 == 0)
                return false;

            System.Numerics.BigInteger D = N - 1;
            while (D % 2 == 0)
                D /= 2;

            for (int k = 0; k < 64; k++)
            {
                if (!MillerRabinTest(N, D))
                    return false;
            }

            return true;
        }

        public static System.Numerics.BigInteger GetFirstPrime(System.Numerics.BigInteger N)
        {
            int Limit = 10000000;
            while (Limit-- > 0)
            {
                if (IsPrime(N))
                    return N;
                N++;
            }
            return 2;
        }

        public static System.Numerics.BigInteger GetLargeRandomPrime()
        {
            byte[] max = Enumerable.Repeat((byte)0xFF, 128).ToArray();

            max[max.Length - 1] &= 0x7F;

            System.Numerics.BigInteger Bmax = new System.Numerics.BigInteger(max);

            System.Numerics.BigInteger N = RandomBigIntInRange(Bmax / 8, Bmax);

            if (IsPrime(N))
                return N;

            else
                return GetFirstPrime(N);
        }

        public static System.Numerics.BigInteger GCD(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;
        }

        public static System.Numerics.BigInteger ModInverse(System.Numerics.BigInteger a, System.Numerics.BigInteger n)
        {
            System.Numerics.BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                System.Numerics.BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }

        public static (RSAKeyClass PublicKey, RSAKeyClass PrivateKey) GenerateKeyPair()
        {
            System.Numerics.BigInteger P = GetLargeRandomPrime();
            System.Numerics.BigInteger Q = GetLargeRandomPrime();
            System.Numerics.BigInteger N = P * Q;
            System.Numerics.BigInteger Phi = (P - 1) * (Q - 1);
            System.Numerics.BigInteger e;
            e = 65537;

            while (GCD(e, Phi) != 1)
            {
                e = GetFirstPrime(e);
            }

            System.Numerics.BigInteger d = ModInverse(e, Phi);

            var PublicKey = new RSAKeyClass(e, N);
            var PrivateKey = new RSAKeyClass(d, N);

            return (PublicKey, PrivateKey);
        }

        public static byte[] Encrypt(byte[] M, RSAKeyClass EncryptionKey) => System.Numerics.BigInteger.ModPow(new System.Numerics.BigInteger(M), EncryptionKey.Key, EncryptionKey.N).ToByteArray();
       
    }
}
