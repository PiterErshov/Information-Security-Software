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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Width = 450;
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
                MessageBox.Show($"Длины текста и ключа не совпадают: Text = {Text.Text.Length}, Key = {Key.Text.Length}");
            if (encryptResults.code == 2)
                MessageBox.Show("Не корректная длина текста или ключа");
            if (encryptResults.code == 1)
                MessageBox.Show("Не корректный формат текста или ключа");
        }

        private void UpdateKey_Click(object sender, RoutedEventArgs e)
        {
            if (KeyFormarFlag == "Rand")
            {
                if (TextFormat.Text == "Text")
                {
                    var bin = ConverteUtility.ConvertStringToByteArray(Text.Text);
                    Key.Text = ConverteUtility.ConvertByteArrayToString(GammaCrypt.RandKey(bin.Length));
                }
                if (TextFormat.Text == "Binary")
                {
                    var bin = ConverteUtility.ConvertBinaryStrToByte(Text.Text);
                    Key.Text = ConverteUtility.ConvertByteArraToBinaryStr(GammaCrypt.RandKey(bin.Length));
                }
                if (TextFormat.Text == "Hexadecimal")
                {
                    var bin = ConverteUtility.HexStringToByteArray(Text.Text);
                    Key.Text = ConverteUtility.ByteArrayToHexString(GammaCrypt.RandKey(bin.Length));
                }
            }
            if (KeyFormarFlag == "Scrambler")
            {
                if (TextFormat.Text == "Text")
                {
                    var start = ConverteUtility.PadToByte(ScramStart.Text);
                    var startbin = ConverteUtility.ConvertBinaryStrToByte(start);
                    var bb = BitConverter.ToUInt16(startbin, 0);
                    var rt = new BitArray(ConverteUtility.ConvertStringToByteArray(Text.Text));
                    var b = ScammblerClass.LFSR_one(rt.Length, Convert.ToUInt32(bb), ScramFormat.SelectedIndex);
                    ScramblerKey = ConverteUtility.GetScramKey(b);
                    Key.Text = ConverteUtility.ConvertByteArrayToString(ConverteUtility.ConvertBinaryStrToByte(ScramblerKey));

                    var hi = ScammblerClass.Hi2(ScramblerKey);
                    var baltest = ScammblerClass.Balance(ScramblerKey);
                    var corrtest = ScammblerClass.Correlation(ScramblerKey);

                    Period.Text = GammaCrypt.Peroid(ScramblerKey).ToString();
                    Hi2Test.Text = hi.ToString();
                    BalansTest.Text = baltest.bal.ToString();

                    if (hi <= HiCrit)
                        HiLabel.Content = "Критерий Hi квадрат пройден";
                    else
                        HiLabel.Content = "Критерий Hi квадрат не пройден";

                    if (baltest.flag)
                        BalansTestLabel.Content = "Последовательность сбалансированная";
                    else
                        BalansTestLabel.Content = "Последовательность сбалансированная";

                    if (corrtest.flag)
                        CorrelTestLabel.Content = "Корреляция присутствует";
                    else
                        CorrelTestLabel.Content = "Корреляция отсутствует";
                }
                if (TextFormat.Text == "Binary")
                {
                    var start = ConverteUtility.PadToByte(ScramStart.Text);
                    var startbin = ConverteUtility.ConvertBinaryStrToByte(start);
                    var bb = BitConverter.ToUInt16(startbin, 0);
                    var rt = new BitArray(ConverteUtility.ConvertStringToByteArray(Text.Text));
                    var b = ScammblerClass.LFSR_one(rt.Length, Convert.ToUInt32(bb), ScramFormat.SelectedIndex);
                    ScramblerKey = ConverteUtility.GetScramKey(b);
                    Key.Text = ScramblerKey;

                    var hi = ScammblerClass.Hi2(ScramblerKey);
                    var baltest = ScammblerClass.Balance(ScramblerKey);
                    var corrtest = ScammblerClass.Correlation(ScramblerKey);

                    Period.Text = GammaCrypt.Peroid(ScramblerKey).ToString();
                    Hi2Test.Text = hi.ToString();
                    BalansTest.Text = baltest.bal.ToString();

                    if (hi <= HiCrit)
                        HiLabel.Content = "Критерий Hi квадрат пройден";
                    else
                        HiLabel.Content = "Критерий Hi квадрат не пройден";

                    if (baltest.flag)
                        BalansTestLabel.Content = "Последовательность сбалансированная";
                    else
                        BalansTestLabel.Content = "Последовательность сбалансированная";

                    if (corrtest.flag)
                        CorrelTestLabel.Content = "Корреляция присутствует";
                    else
                        CorrelTestLabel.Content = "Корреляция отсутствует";
                }
                if (TextFormat.Text == "Hexadecimal")
                {
                    var start = ConverteUtility.PadToByte(ScramStart.Text);
                    var startbin = ConverteUtility.ConvertBinaryStrToByte(start);
                    var bb = BitConverter.ToUInt16(startbin, 0);
                    var rt = new BitArray(ConverteUtility.ConvertStringToByteArray(Text.Text));
                    var b = ScammblerClass.LFSR_one(rt.Length, Convert.ToUInt32(bb), ScramFormat.SelectedIndex);
                    ScramblerKey = ConverteUtility.GetScramKey(b);
                    Key.Text = ConverteUtility.ByteArrayToHexString(ConverteUtility.ConvertBinaryStrToByte(ScramblerKey));

                    var hi = ScammblerClass.Hi2(ScramblerKey);
                    var baltest = ScammblerClass.Balance(ScramblerKey);
                    var corrtest = ScammblerClass.Correlation(ScramblerKey);

                    Period.Text = GammaCrypt.Peroid(ScramblerKey).ToString();
                    Hi2Test.Text = hi.ToString();
                    BalansTest.Text = baltest.bal.ToString();

                    if (hi <= HiCrit)
                        HiLabel.Content = "Критерий Hi квадрат пройден";
                    else
                        HiLabel.Content = "Критерий Hi квадрат не пройден";

                    if (baltest.flag)
                        BalansTestLabel.Content = "Последовательность сбалансированная";
                    else
                        BalansTestLabel.Content = "Последовательность сбалансированная";

                    if (corrtest.flag)
                        CorrelTestLabel.Content = "Корреляция присутствует";
                    else
                        CorrelTestLabel.Content = "Корреляция отсутствует";
                }
            }
        }

        private void KeyType_DropDownClosed(object sender, EventArgs e)
        {
            if (KeyType.Text == "Случайный Ключ")
            {
                Width = 450;
                KeyFormarFlag = "Rand";
            }
            else
            {
                Width = 900;
                KeyFormarFlag = "Scrambler";
                var scramstart = FileUtility.DeserializeString<ScramModel>(FileUtility.JSONSrt(ScramFile));
                if (scramstart.Scram != "" && scramstart.Scram.Length == 9)
                    ScramStart.Text = scramstart.Scram;
                else
                {
                    MessageBox.Show("Начальное значение скремблера не корректно");
                    ScramStart.Text += ConverteUtility.GenStartVal(9);
                }
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

        private void ScramStart_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (ScramStart.Text.Length != 9 || !ConverteUtility.CheckIncorrectFormat(ScramStart.Text, "Bin"))
                ScramStart.Text = ConverteUtility.GenStartVal(9);
        }

        private void DeciphButton_Click(object sender, RoutedEventArgs e)
        {
            Text.Text = Chiphrtext.Text;
            Chiphrtext.Clear();

            var encryptResults = GammaCrypt.Gamming(Text.Text, Key.Text, TextFormat.Text);
            if (encryptResults.code == 0)
                Chiphrtext.Text = encryptResults.output;
            if (encryptResults.code == 3)
                MessageBox.Show($"Длины текста и ключа не совпадают: Text = {Text.Text.Length}, Key = {Key.Text.Length}");
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

        private void StartScram_Click(object sender, RoutedEventArgs e)
        {
            var scramstart = FileUtility.DeserializeString<ScramModel>(FileUtility.JSONSrt(ScramFile));
            if (scramstart.Scram != "")
                ScramStart.Text = scramstart.Scram;
            else
                MessageBox.Show("Начальное значение скремблера не задано");
        }

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
