using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections;
using Lab1_Gamming_Srammbling.CryptoClass;
using Lab1_Gamming_Srammbling.Models;
using Lab1_Gamming_Srammbling.Utilitiets;

namespace Lab1_Gamming_Srammbling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string TextFormarFlag = "Text";
        private string ResourceFile = "Data.json";

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Width = 450;
        }

        private void TextFormat_DropDownClosed(object sender, EventArgs e)
        {
            if (TextFormarFlag == "Text")
            {
                Text.Text = GammaCrypt.UniConvert(Text.Text, TextFormarFlag, TextFormat.Text);
                Key.Text = GammaCrypt.UniConvert(Key.Text, TextFormarFlag, TextFormat.Text);
                Chiphrtext.Text = GammaCrypt.UniConvert(Chiphrtext.Text, TextFormarFlag, TextFormat.Text);
            }

            if (TextFormarFlag == "Binary")
                if (GammaCrypt.CheckIncorrectFormat(Text.Text, "Bin") && GammaCrypt.CheckIncorrectLength(Text.Text))
                {
                    Text.Text = GammaCrypt.UniConvert(Text.Text, TextFormarFlag, TextFormat.Text);
                    Key.Text = GammaCrypt.UniConvert(Key.Text, TextFormarFlag, TextFormat.Text);
                    Chiphrtext.Text = GammaCrypt.UniConvert(Chiphrtext.Text, TextFormarFlag, TextFormat.Text);
                }
                else
                {
                    MessageBox.Show("Не корректный формат");
                    TextFormat.SelectedIndex = 1;
                }

            if (TextFormarFlag == "Hexadecimal")
                if (GammaCrypt.CheckIncorrectFormat(Text.Text, "Hex") && GammaCrypt.CheckIncorrectLength(Text.Text))
                {
                    Text.Text = GammaCrypt.UniConvert(Text.Text, TextFormarFlag, TextFormat.Text);
                    Key.Text = GammaCrypt.UniConvert(Key.Text, TextFormarFlag, TextFormat.Text);
                    Chiphrtext.Text = GammaCrypt.UniConvert(Chiphrtext.Text, TextFormarFlag, TextFormat.Text);
                }
                else
                {
                    MessageBox.Show("Не корректный формат");
                    TextFormat.SelectedIndex = 2;
                }
        }

        private void TextFormat_DropDownOpened(object sender, EventArgs e)
        {
            TextFormarFlag = TextFormat.Text;
        }

        private void CiphButton_Click(object sender, RoutedEventArgs e)
        {
            var encryptResults = GammaCrypt.Gamming(Text.Text, Key.Text, TextFormat.Text);
            if (encryptResults.code == 0)
                Chiphrtext.Text = encryptResults.output;
            if (encryptResults.code == 3)
                MessageBox.Show("Длины текста и ключа не совпадают");
            if (encryptResults.code == 2)
                MessageBox.Show("Не корректная длина текста или ключа");
            if (encryptResults.code == 1)
                MessageBox.Show("Не корректный формат текста или ключа");
        }

        private void UpdateKey_Click(object sender, RoutedEventArgs e)
        {
            if (TextFormat.Text == "Text")
                Key.Text = GammaCrypt.ConvertByteArrayToString(GammaCrypt.RandKey(Text.Text.Length));
            if (TextFormat.Text == "Binary")
                Key.Text = GammaCrypt.ConvertByteArraToBinaryStr(GammaCrypt.RandKey(Text.Text.Length));
            if (TextFormat.Text == "Hexadecimal")
                Key.Text = GammaCrypt.ByteArrayToHexString(GammaCrypt.RandKey(Text.Text.Length));
        }

        private void KeyType_DropDownClosed(object sender, EventArgs e)
        {
            if (KeyType.Text == "Случайный Ключ")
                Width = 450;
            else
                Width = 700;
        }

        private void ScramFormat_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ScramStart.Clear();
            if (ScramFormat.Text != "")
            {
                var scrambler = ScramFormat.Text.Replace(" ", "");
                ScramStart.Text += GammaCrypt.GenStartVal(Convert.ToInt32(scrambler[0].ToString()));
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var data = new DataModel();
            data.text = Text.Text;
            data.key = Key.Text;
            data.chiphr = Chiphrtext.Text;
            var str = FileUtility.Serialize(data);
            FileUtility.JSONSave(ResourceFile, str);
        }

        private void FileLoad_Click(object sender, RoutedEventArgs e)
        {
            Text.Clear();
            Key.Clear();
            Chiphrtext.Clear();
            var data = FileUtility.DeserializeString(FileUtility.JSONSrt(ResourceFile));
            Text.Text = data.text;
            Key.Text = data.key;
            Chiphrtext.Text = data.chiphr;
        }
    }
}
