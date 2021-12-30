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


        public static byte[] FS_function(string key, byte[] left_block)
        {
            var Sxor = new byte[left_block.Length];
            var bb = BitConverter.ToUInt16(ShiftKey(key, 0, 8), 0);
            var start = Convert.ToUInt32(bb);
            var ublock = ScammblerClass.LFSR_one(start, 0);
            var roundKey = ConverteUtility.ConvertBinaryStrToByte(ConverteUtility.GetScramKey(ublock));
            new BitArray(roundKey).Xor(new BitArray(left_block)).CopyTo(Sxor, 0);
            return Sxor;
        }

        public static byte[] ShiftKey(string key, int i, int lenght = 32)
        {
            string halfKey = "";
            int j = 0;

            while(j < lenght)
            {
                if (i == key.Length)
                    i = 0;
                halfKey += key[i];
                i++;
                j++;
            }

            return ConverteUtility.ConvertBinaryStrToByte(halfKey);
        }

        public static byte[] Feistel_Network(byte[] text, string key, int keytype = 0, int funktype = 0)
        {
            var chiphrText = new byte[text.Length];
            uint start;
            for (int i = 0; i < text.Length; i += 8)
            {                
                var block = text.Skip(i).ToArray().Take(8).ToArray();
                var halfBlocks = GetHalfs(block);
                var right = new byte[halfBlocks.second.Length];
                var left = new byte[halfBlocks.first.Length];                
                for (int j = 0; j < SyclLenght; j++)
                {
                    var roundKey = new byte[block.Length];
                    var buff = new byte[halfBlocks.second.Length];
                    if (keytype == 0)
                    {
                        roundKey = ShiftKey(key, j);
                    } 
                    else
                    {
                        var bb = BitConverter.ToUInt16(ShiftKey(key, j, 8), 0);
                        start = Convert.ToUInt32(bb);
                        var ublock = ScammblerClass.LFSR_one(start, 0);
                        roundKey = ConverteUtility.ConvertBinaryStrToByte(ConverteUtility.GetScramKey(ublock));
                    }
                    if(funktype == 0)
                    {
                        buff = left;                        
                        new BitArray(roundKey).Xor(new BitArray(right)).CopyTo(left, 0);
                        right = buff;
                    }
                    else
                    {
                        buff = left;
                        var FS = FS_function(key, left);
                        new BitArray(FS).Xor(new BitArray(right)).CopyTo(left, 0);
                        right = buff;
                    }                     
                }
                Array.Copy(left, 0, chiphrText, i, left.Length);
                Array.Copy(right, 0, chiphrText, i + 8, right.Length);
            }
            return chiphrText;
        }

    }
}
