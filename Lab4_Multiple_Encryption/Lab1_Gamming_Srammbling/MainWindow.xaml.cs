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
        private string OldTextFormarFlag = "Text";
        private string KeyFormarFlag = "Rand";
        private string ResourceFile = "Data.json";
        private string TextFile = "Text.json";
        private string KeyFile = "Key.json";
        private string ChiphrFile = "ChiphrText.json";
        private string Chiphrmod = "BC";
        private byte[] TextArray = null;
        private byte[] KeyArray = null;
        private byte[] ChiphrArray = null;
        private byte[] IVArray = null;
        private byte[] SecKeyArray = null;
        private bool Flag = true;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SecKey.Visibility = Visibility.Hidden;
            SecKey.IsEnabled = false;
            SecKeyLabel.Visibility = Visibility.Hidden;
            SecKeyLabel.IsEnabled = false;
            UpdateSecKey.Visibility = Visibility.Hidden;
            UpdateSecKey.IsEnabled = false;
            if (Flag == true)
            {
                ChiphrLabel.Margin = new Thickness(ChiphrLabel.Margin.Left, ChiphrLabel.Margin.Top - 100, ChiphrLabel.Margin.Right, ChiphrLabel.Margin.Bottom);
                Chiphrtext.Margin = new Thickness(Chiphrtext.Margin.Left, Chiphrtext.Margin.Top - 100, Chiphrtext.Margin.Right, Chiphrtext.Margin.Bottom);
                CiphButton.Margin = new Thickness(CiphButton.Margin.Left, CiphButton.Margin.Top - 100, CiphButton.Margin.Right, CiphButton.Margin.Bottom);
                DeciphButton.Margin = new Thickness(DeciphButton.Margin.Left, DeciphButton.Margin.Top - 100, DeciphButton.Margin.Right, DeciphButton.Margin.Bottom);
                SaveFile.Margin = new Thickness(SaveFile.Margin.Left, SaveFile.Margin.Top - 100, SaveFile.Margin.Right, SaveFile.Margin.Bottom);
                FileLoad.Margin = new Thickness(FileLoad.Margin.Left, FileLoad.Margin.Top - 100, FileLoad.Margin.Right, FileLoad.Margin.Bottom);
                LoadChiphFile.Margin = new Thickness(LoadChiphFile.Margin.Left, LoadChiphFile.Margin.Top - 100, LoadChiphFile.Margin.Right, LoadChiphFile.Margin.Bottom);
                Flag = false;
            }
        }

        private void TextFormat_DropDownClosed(object sender, EventArgs e)
        {

            if (TextFormarFlag == "Text")
            {
                Text.Text = ConverteUtility.UniConvert(Text.Text, TextFormarFlag, TextFormat.Text);
                Key.Text = ConverteUtility.UniConvert(Key.Text, TextFormarFlag, TextFormat.Text);
                Chiphrtext.Text = ConverteUtility.UniConvert(Chiphrtext.Text, TextFormarFlag, TextFormat.Text);
            }

            if (TextFormarFlag == "Binary")
                if (ConverteUtility.CheckIncorrectFormat(Text.Text, "Bin") && ConverteUtility.CheckIncorrectLength(Text.Text))
                {
                    Text.Text = ConverteUtility.UniConvert(Text.Text, TextFormarFlag, TextFormat.Text);
                    Key.Text = ConverteUtility.UniConvert(Key.Text, TextFormarFlag, TextFormat.Text);
                    Chiphrtext.Text = ConverteUtility.UniConvert(Chiphrtext.Text, TextFormarFlag, TextFormat.Text);
                }
                else
                {
                    MessageBox.Show("Не корректный формат");
                    TextFormat.SelectedIndex = 1;
                }

            if (TextFormarFlag == "Hexadecimal")
                if (ConverteUtility.CheckIncorrectFormat(Text.Text, "Hex") && ConverteUtility.CheckIncorrectLength(Text.Text))
                {
                    Text.Text = ConverteUtility.UniConvert(Text.Text, TextFormarFlag, TextFormat.Text);
                    Key.Text = ConverteUtility.UniConvert(Key.Text, TextFormarFlag, TextFormat.Text);
                    Chiphrtext.Text = ConverteUtility.UniConvert(Chiphrtext.Text, TextFormarFlag, TextFormat.Text);
                }
                else
                {
                    MessageBox.Show("Не корректный формат");
                    TextFormat.SelectedIndex = 2;
                }

                OldTextFormarFlag = TextFormat.Text;
        }

        private void TextFormat_DropDownOpened(object sender, EventArgs e)
        {
            TextFormarFlag = TextFormat.Text;
            OldTextFormarFlag = TextFormat.Text;
        }

        private void CiphButton_Click(object sender, RoutedEventArgs e)
        {
            var encryptResults = AESClass.Converter(TextArray, KeyArray, TextFormat.Text, "encrypt", Chiphrmod, IVArray, SecKeyArray);
            ChiphrArray = encryptResults.chipout;
            if (encryptResults.code == 0)
                Chiphrtext.Text = encryptResults.output;          
            if (encryptResults.code == 2)
                MessageBox.Show("Не корректная длина текста или ключа");
            if (encryptResults.code == 1)
                MessageBox.Show("Не корректный формат текста или ключа");
        }

        private void UpdateKey_Click(object sender, RoutedEventArgs e)
        {
            KeyArray = AESClass.RandKey();

            if (TextFormat.Text == "Text")
            {
                Key.Text = ConverteUtility.ConvertByteArrayToString(AESClass.RandKey());
            }
            if (TextFormat.Text == "Binary")
            {
                Key.Text = ConverteUtility.ConvertByteArraToBinaryStr(AESClass.RandKey());
            }
            if (TextFormat.Text == "Hexadecimal")
            {
                Key.Text = ConverteUtility.ByteArrayToHexString(AESClass.RandKey());
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var text = new TextModel();
            var chiphr = new ChiphrModel();
            var key = new KeyModel();

            text.Text = Text.Text;
            key.Key = Key.Text;
            chiphr.Chiphr = Chiphrtext.Text;

            if (text.Text != "")
                FileUtility.JSONSave(TextFile, FileUtility.Serialize(text));
            else
                MessageBox.Show("Добавте текст");

            if (key.Key != "")
                FileUtility.JSONSave(KeyFile, FileUtility.Serialize(key));
            else
                MessageBox.Show("Добавте ключ");

            if (chiphr.Chiphr != "")
                FileUtility.JSONSave(ChiphrFile, FileUtility.Serialize(chiphr));
            else
                MessageBox.Show("Добавте шифротекст");

        }

        private void FileLoad_Click(object sender, RoutedEventArgs e)
        {
            Text.Clear();
            Key.Clear();
            Chiphrtext.Clear();
            var text = FileUtility.DeserializeString<TextModel>(FileUtility.JSONSrt(TextFile));
            var key = FileUtility.DeserializeString<KeyModel>(FileUtility.JSONSrt(KeyFile));
            Text.Text = text.Text;
            Key.Text = key.Key;
        }


        private void DeciphButton_Click(object sender, RoutedEventArgs e)
        {
            Text.Text = Chiphrtext.Text;
            TextArray = ChiphrArray;
            Chiphrtext.Clear();

            var encryptResults = AESClass.Converter(TextArray, KeyArray, TextFormat.Text, "decryt", Chiphrmod, IVArray, SecKeyArray);
            if (encryptResults.code == 0)
                Chiphrtext.Text = encryptResults.output;            
            if (encryptResults.code == 2)
                MessageBox.Show("Не корректная длина текста или ключа");
            if (encryptResults.code == 1)
                MessageBox.Show("Не корректный формат текста или ключа");
        }

        private void LoadChiphFile_Click(object sender, RoutedEventArgs e)
        {
            Text.Clear();
            Key.Clear();
            Chiphrtext.Clear();
            var chiphr = FileUtility.DeserializeString<ChiphrModel>(FileUtility.JSONSrt(ChiphrFile));
            var key = FileUtility.DeserializeString<KeyModel>(FileUtility.JSONSrt(KeyFile));
            Text.Text = chiphr.Chiphr;
            Key.Text = key.Key;
        }

        private void Text_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Text.Text != "" && TextFormat.Text == OldTextFormarFlag)
            {
                if (TextFormat.Text == "Text")
                    TextArray = ConverteUtility.ConvertStringToByteArray(Text.Text);
                if (TextFormat.Text == "Binary")
                    TextArray = ConverteUtility.ConvertBinaryStrToByte(Text.Text);
                if (TextFormat.Text == "Hexadecimal")
                    TextArray = ConverteUtility.HexStringToByteArray(Text.Text);
            }
        }

        private void Key_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Key.Text != "" && TextFormat.Text == OldTextFormarFlag)
            {
                if (TextFormat.Text == "Text")
                    KeyArray = ConverteUtility.ConvertStringToByteArray(Key.Text);
                if (TextFormat.Text == "Binary")
                    KeyArray = ConverteUtility.ConvertBinaryStrToByte(Key.Text);
                if (TextFormat.Text == "Hexadecimal")
                    KeyArray = ConverteUtility.HexStringToByteArray(Key.Text);
            }
        }

        private void UpdateIV_Click(object sender, RoutedEventArgs e)
        {
            IVArray = AESClass.RandKey();

            if (TextFormat.Text == "Text")
            {
                IV.Text = ConverteUtility.ConvertByteArrayToString(IVArray);
            }
            if (TextFormat.Text == "Binary")
            {
                IV.Text = ConverteUtility.ConvertByteArraToBinaryStr(IVArray);
            }
            if (TextFormat.Text == "Hexadecimal")
            {
                IV.Text = ConverteUtility.ByteArrayToHexString(IVArray);
            }
        }



        private void ChiphrMod_DropDownClosed(object sender, EventArgs e)
        {
            if(ChiphrMod.Text == "BC")
            {
                SecKey.Visibility = Visibility.Hidden;
                SecKey.IsEnabled = false;
                SecKeyLabel.Visibility = Visibility.Hidden;
                SecKeyLabel.IsEnabled = false;
                UpdateSecKey.Visibility = Visibility.Hidden;
                UpdateSecKey.IsEnabled = false;
                if (Flag == true)
                {
                    ChiphrLabel.Margin = new Thickness(ChiphrLabel.Margin.Left, ChiphrLabel.Margin.Top - 100, ChiphrLabel.Margin.Right, ChiphrLabel.Margin.Bottom);
                    Chiphrtext.Margin = new Thickness(Chiphrtext.Margin.Left, Chiphrtext.Margin.Top - 100, Chiphrtext.Margin.Right, Chiphrtext.Margin.Bottom);
                    CiphButton.Margin = new Thickness(CiphButton.Margin.Left, CiphButton.Margin.Top - 100, CiphButton.Margin.Right, CiphButton.Margin.Bottom);
                    DeciphButton.Margin = new Thickness(DeciphButton.Margin.Left, DeciphButton.Margin.Top - 100, DeciphButton.Margin.Right, DeciphButton.Margin.Bottom);
                    SaveFile.Margin = new Thickness(SaveFile.Margin.Left, SaveFile.Margin.Top - 100, SaveFile.Margin.Right, SaveFile.Margin.Bottom);
                    FileLoad.Margin = new Thickness(FileLoad.Margin.Left, FileLoad.Margin.Top - 100, FileLoad.Margin.Right, FileLoad.Margin.Bottom);
                    LoadChiphFile.Margin = new Thickness(LoadChiphFile.Margin.Left, LoadChiphFile.Margin.Top - 100, LoadChiphFile.Margin.Right, LoadChiphFile.Margin.Bottom);
                    Flag = false;
                }
            }
            else
            {
                SecKey.Visibility = Visibility.Visible;
                SecKey.IsEnabled = true;
                SecKeyLabel.Visibility = Visibility.Visible;
                SecKeyLabel.IsEnabled = true;
                UpdateSecKey.Visibility = Visibility.Visible;
                UpdateSecKey.IsEnabled = true;
                if (Flag == false)
                {
                    ChiphrLabel.Margin = new Thickness(ChiphrLabel.Margin.Left, ChiphrLabel.Margin.Top + 100, ChiphrLabel.Margin.Right, ChiphrLabel.Margin.Bottom);
                    Chiphrtext.Margin = new Thickness(Chiphrtext.Margin.Left, Chiphrtext.Margin.Top + 100, Chiphrtext.Margin.Right, Chiphrtext.Margin.Bottom);
                    CiphButton.Margin = new Thickness(CiphButton.Margin.Left, CiphButton.Margin.Top + 100, CiphButton.Margin.Right, CiphButton.Margin.Bottom);
                    DeciphButton.Margin = new Thickness(DeciphButton.Margin.Left, DeciphButton.Margin.Top + 100, DeciphButton.Margin.Right, DeciphButton.Margin.Bottom);
                    SaveFile.Margin = new Thickness(SaveFile.Margin.Left, SaveFile.Margin.Top + 100, SaveFile.Margin.Right, SaveFile.Margin.Bottom);
                    FileLoad.Margin = new Thickness(FileLoad.Margin.Left, FileLoad.Margin.Top + 100, FileLoad.Margin.Right, FileLoad.Margin.Bottom);
                    LoadChiphFile.Margin = new Thickness(LoadChiphFile.Margin.Left, LoadChiphFile.Margin.Top + 100, LoadChiphFile.Margin.Right, LoadChiphFile.Margin.Bottom);
                    Flag = true;
                }
            }
        }

        private void TextFormat_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void UpdateSecKey_Click(object sender, RoutedEventArgs e)
        {
            SecKeyArray = AESClass.RandKey();
            if (TextFormat.Text == "Text")
            {
                SecKey.Text = ConverteUtility.ConvertByteArrayToString(SecKeyArray);
            }
            if (TextFormat.Text == "Binary")
            {
                SecKey.Text = ConverteUtility.ConvertByteArraToBinaryStr(SecKeyArray);
            }
            if (TextFormat.Text == "Hexadecimal")
            {
                SecKey.Text = ConverteUtility.ByteArrayToHexString(SecKeyArray);
            }
        }
    }
}
