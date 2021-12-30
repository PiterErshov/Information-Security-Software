using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml;

namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public class ElGamal : AsymmetricAlgorithm
    {
        private ElGamalKeyStruct keyStruct;
        public ElGamal()
        {
            keyStruct = new ElGamalKeyStruct();
            keyStruct.P = new BigInteger(0);
            keyStruct.G = new BigInteger(0);
            keyStruct.Y = new BigInteger(0);
            keyStruct.X = new BigInteger(0);
            KeySizeValue = 1024;
            LegalKeySizesValue = new KeySizes[] { new KeySizes(256, 1024, 8) };
        }
        /// <summary>
        /// This method contains .Net framework methods that are specially added for generation of pseudo-
        /// prime numbers and random bits
        /// </summary>
        /// <param name="nrOfBits"></param>
        private void CreateKeyPair(int nrOfBits)
        {
            Random randomGenerator = new Random();

            keyStruct.P = BigInteger.genPseudoPrime(nrOfBits, 20, randomGenerator);

            keyStruct.X = new BigInteger();
            keyStruct.X.genRandomBits(nrOfBits - 1, randomGenerator);
            keyStruct.G = new BigInteger();
            keyStruct.G.genRandomBits(nrOfBits - 1, randomGenerator);

            keyStruct.Y = keyStruct.G.modPow(keyStruct.X, keyStruct.P);
        }

        

        private bool NeedToGenerateKey()
        {
            return keyStruct.P == 0 && keyStruct.G == 0 && keyStruct.Y == 0;
        }
        
        public byte[] Sign(byte[] hashCode)
        {
            if (NeedToGenerateKey())
            {
                CreateKeyPair(KeySizeValue);
            }

            return ElGamalSignature.CreateSignature(hashCode, keyStruct);

        }
        public bool VerifySignature(byte[] hashCode, byte[] signature)
        {
            if (NeedToGenerateKey())
            {
                CreateKeyPair(KeySizeValue);
            }
            return ElGamalSignature.VerifySignature(hashCode, signature, keyStruct);
        }        
    }
}
