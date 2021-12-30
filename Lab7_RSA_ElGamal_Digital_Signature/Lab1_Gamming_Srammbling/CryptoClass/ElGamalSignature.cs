using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public static class ElGamalSignature
    {
        public static BigInteger module(BigInteger number, BigInteger moduleValue)
        {
            BigInteger result = number % moduleValue;
            if (result < 0)
            {
                result += moduleValue;
            }
            return result;
        }

        public static ElGamalKeyStruct GenModKey(BigInteger X, BigInteger G, BigInteger P)
        {
            var out_key = new ElGamalKeyStruct();
            out_key.X = X;
            out_key.G = G;
            out_key.P = P;
            out_key.Y = G.modPow(X, P);

            return out_key;
        }

        public static byte[] CreateSignature(byte[] pData, ElGamalKeyStruct keyStruct, BigInteger K_in = null)
        {
            IList<byte> data = pData;
            BigInteger KValuesRange = keyStruct.P - 1;
            BigInteger K;
            /*
            if (K_in != null)
            {
                do
                {
                    K = new BigInteger();
                    K.genRandomBits(keyStruct.P.bitCount() - 1, new Random());
                } while (K.gcd(KValuesRange) != 1);
            }
            else*/
            K = K_in;

            BigInteger A = keyStruct.G.modPow(K, keyStruct.P);
            BigInteger B = module(K.modInverse(KValuesRange) * (new BigInteger(data) - (keyStruct.X * A)), KValuesRange);

            byte[] aInBytes = A.getBytes();
            byte[] bInBytes = B.getBytes();

            int size = (((keyStruct.P.bitCount() + 7) / 8) * 2);
            byte[] result = new byte[size];

            Array.Copy(aInBytes, 0, result, size / 2 - aInBytes.Length, aInBytes.Length);
            Array.Copy(bInBytes, 0, result, size - bInBytes.Length, bInBytes.Length);


            return result;
        }
        public static bool VerifySignature(byte[] data, byte[] signature, ElGamalKeyStruct keyStruct)
        {
            int size = signature.Length / 2;
            byte[] aInBytes = new byte[size];
            Array.Copy(signature, 0, aInBytes, 0, aInBytes.Length);

            byte[] bInBytes = new byte[size];
            Array.Copy(signature, size, bInBytes, 0, bInBytes.Length);

            BigInteger A = new BigInteger(aInBytes);
            BigInteger B = new BigInteger(bInBytes);

            BigInteger e1 = module(keyStruct.Y.modPow(A, keyStruct.P) * A.modPow(B, keyStruct.P), keyStruct.P);

            BigInteger e2 = keyStruct.G.modPow(new BigInteger(data), keyStruct.P);

            return e1 == e2;
        }
    }
}
