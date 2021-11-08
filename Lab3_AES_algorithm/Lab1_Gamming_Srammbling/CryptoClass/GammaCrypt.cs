using Lab1_Gamming_Srammbling.Utilitiets;
using System;
using System.Collections;
using System.Text;


namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public static class GammaCrypt
    {
        public static byte[] RandKey(int leng) //Случайный ключ
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            var bt = new Byte[leng];
            rnd.NextBytes(bt);
            return bt;
        }

        public static byte[] Encryptor(byte[] text, byte[] key) //Шифровщик/дешифровщик
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var cipherText = new byte[text.Length];
            new BitArray(key).Xor(new BitArray(text)).CopyTo(cipherText, 0);
            return cipherText;
        }

        public static (string output, int code) Gamming(string Text, string Key, string flag = "Text") //Универсальный преобразователь
        {
            string chiphrtext = "";
            int code = 0;
            if (flag == "Text")
            {
                if (Text.Length == Key.Length)
                {
                    var text = ConverteUtility.ConvertStringToByteArray(Text);
                    var key = ConverteUtility.ConvertStringToByteArray(Key);
                    var tt = Encryptor(text, key);
                    chiphrtext = ConverteUtility.ConvertByteArrayToString(tt);
                }
                else
                    code = 3;
            }

            if (flag == "Binary")
            {
                if (ConverteUtility.CheckIncorrectFormat(Text, "Bin") && ConverteUtility.CheckIncorrectFormat(Key, "Bin"))
                {
                    if (ConverteUtility.CheckIncorrectLength(Text) && ConverteUtility.CheckIncorrectLength(Key))
                    {
                        if (Text.Length == Key.Length)
                        {
                            var text = ConverteUtility.ConvertBinaryStrToByte(Text);
                            var key = ConverteUtility.ConvertBinaryStrToByte(Key);
                            chiphrtext = ConverteUtility.ConvertByteArraToBinaryStr(Encryptor(text, key));
                        }
                        else
                            code = 3;
                    }
                    else
                        code = 2;
                }
                else
                    code = 1;
            }
            if (flag == "Hexadecimal")
            {
                if (ConverteUtility.CheckIncorrectFormat(Text, "Hex") && ConverteUtility.CheckIncorrectFormat(Key, "Hex"))
                {
                    if (ConverteUtility.CheckIncorrectLength(Text) && ConverteUtility.CheckIncorrectLength(Key))
                    {
                        if (Text.Length == Key.Length)
                        {
                            var text = ConverteUtility.HexStringToByteArray(Text);
                            var key = ConverteUtility.HexStringToByteArray(Key);
                            chiphrtext = ConverteUtility.ByteArrayToHexString(Encryptor(text, key));
                        }
                        else
                            code = 3;
                    }
                    else
                        code = 2;
                }
                else
                    code = 1;
            }

            return (chiphrtext, code);
        }
        public static int Peroid(string seq) //Вычисление периода
        {
            int per = 1;
            int step = 0;
            while (step + per != seq.Length)
            {
                if (seq[step] != seq[per + step])
                {
                    ++per;
                    step = 0;
                }
                else
                {
                    ++step;
                }
            }
            return per;
        }

    }
}
