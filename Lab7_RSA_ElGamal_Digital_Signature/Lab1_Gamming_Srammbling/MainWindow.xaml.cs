using Lab1_Gamming_Srammbling.CryptoClass;
using Lab1_Gamming_Srammbling.Models;
using Lab1_Gamming_Srammbling.Utilitiets;
using System;
using System.Numerics;
using System.Windows;

namespace Lab1_Gamming_Srammbling
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
          
        private string PUFile = "RSAPublicKey.json";
        private string PVFile = "RSAPrivateKey.json"; 
        private RSAKeyClass PU, PV;
        private byte[] Sk = null;
        private int KeyLenght = 16;

        private void CiphButton_Click(object sender, RoutedEventArgs e)
        {
            //var encryptResults = AESClass.Converter(TextArray, KeyArray, TextFormat.Text, "encrypt", Chiphrmod, IVArray, SecKeyArray);
            PV = new RSAKeyClass(new System.Numerics.BigInteger(5), new System.Numerics.BigInteger(65));
            Sk = RSAClass.Encrypt(BitConverter.GetBytes(Convert.ToInt32(Text.Text)), PV);
            byte[] tt = new byte[4];
            tt[0] = Sk[0];
            Chiphrtext.Text = BitConverter.ToInt32(tt, 0).ToString();
        }

        private void ElGamVerification_Click(object sender, RoutedEventArgs e)
        {
            var elGamKey = ElGamalSignature.GenModKey(new BigInteger(Convert.ToInt32(ElGamX.Text)), 
                                                      new BigInteger(Convert.ToInt32(ElGamG.Text)), 
                                                      new BigInteger(Convert.ToInt32(ElGamP.Text)));
            var signature = ElGamalSignature.CreateSignature(BitConverter.GetBytes(Convert.ToInt32(ElGamHesh.Text)), elGamKey, 
                                                             new BigInteger(Convert.ToInt32(ElGamK.Text)));

            ElGamRes.Text = ElGamalSignature.VerifySignature(
                            BitConverter.GetBytes(Convert.ToInt32(ElGamHesh.Text)), signature, elGamKey).ToString();
        }

        private void RSAGen_Click(object sender, RoutedEventArgs e)
        {
            var tt = RSAClass.GenerateKeyPair();
            PU = tt.PublicKey;
            PV = tt.PrivateKey;            
        }
    }
}
