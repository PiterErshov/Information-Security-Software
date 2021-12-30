using Lab1_Gamming_Srammbling.CryptoClass;
using Lab1_Gamming_Srammbling.Models;
using Lab1_Gamming_Srammbling.Utilitiets;
using System;
using System.Collections;
using System.Windows;

namespace Lab1_Gamming_Srammbling
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string TextFormarFlag = "Text";
        private string KeyFormarFlag = "Rand";
        private string ResourceFile = "Data.json";
        private string TextFile = "Text.json";
        private string KeyFile = "Key.json";
        private string ChiphrFile = "ChiphrText.json";
        private string ScramFile = "ScramStart.json";
        private string ScramblerKey = "";
        private double HiCrit = 3.842;
        private void AESRUN_Click(object sender, RoutedEventArgs e)
        {
            var text = ConverteUtility.HexStringToByteArray(AESText.Text);
            var key = ConverteUtility.HexStringToByteArray(AESKey.Text);

            var output = AESClass.OneRound(text, key);

            S1.Text = ConverteUtility.ByteArrayToHexString(output.s1);
            S2.Text = ConverteUtility.ByteArrayToHexString(output.s2);
            S3.Text = ConverteUtility.ByteArrayToHexString(output.s3);
            S4.Text = ConverteUtility.ByteArrayToHexString(output.s4);
            S5.Text = ConverteUtility.ByteArrayToHexString(output.s5);
        }
    }
}
