using Lab2_BCP_Feistel_network.Utilitiets;
using System;
using System.Collections;
using System.Linq;
using System.Text;


namespace Lab2_BCP_Feistel_network.CryptoClass
{
    public static class GammaCrypt
    {
        private static int SyclLenght = 16;
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

        public static (byte[] first, byte[] second) GetHalfs(byte[] text) =>
            (text.Take(text.Length / 2).ToArray(), text.Skip(text.Length / 2).ToArray());


        public static byte[] FS_function(byte[] halfKey, byte[] left_block)
        {
            var Sxor = new byte[left_block.Length];
            new BitArray(halfKey).Xor(new BitArray(left_block)).CopyTo(Sxor, 0);
            return Sxor;
        }



        public static byte[] F_function(byte[] halfKey, byte[] left_block) => halfKey;

        public static byte[] ShiftKey(string key, int i)
        {
            var half = new byte[key.Length];

            return half;
        }

        public static byte[] Feistel_Network(byte[] text, uint start, int keytype = 0, int funktype = 0)
        {
            var chiphrText = new byte[text.Length];

            for (int i = 0; i < text.Length; i += 8)
            {                
                var block = text.Skip(i).ToArray().Take(8).ToArray();
                var halfBlocks = GetHalfs(block);
                var right = new byte[halfBlocks.second.Length];
                var left = new byte[halfBlocks.first.Length];
                for (int j = 0; j < SyclLenght; j++)
                {
                    var buff = new byte[halfBlocks.second.Length];
                    if (keytype == 0)
                    {

                    }
                      
                   
                }
            }

            return chiphrText;

        }

    }
}
