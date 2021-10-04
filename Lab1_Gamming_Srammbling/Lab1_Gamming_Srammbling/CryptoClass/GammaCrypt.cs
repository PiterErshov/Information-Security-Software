using System;
using System.Text;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public static class GammaCrypt
    {
        private const string AllowedСharHex = "0123456789ABCDEF";

        private const string AllowedСharBin = "01";

        private static Encoding enc = Encoding.UTF8;
        public static byte[] RandKey(int leng)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            var bt = new Byte[leng];
            rnd.NextBytes(bt);
            return bt;
        }

        public static bool CheckIncorrectFormat(string text, string format = "Bin")
        {
            bool flag = false;
            string sample = "";
            string buff = text.Replace(" ", "");
            switch (format)
            {
                case "Bin":
                    {
                        sample = AllowedСharBin;
                        break;
                    }
                case "Hex":
                    {
                        sample = AllowedСharHex;
                        break;
                    }
            }
            foreach(var i in buff)
            {
                flag = false;
                foreach (var j in sample)
                {
                    if (i == j)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)                
                    break;
            }
            return flag;
        }

        public static bool CheckIncorrectLength(string text)
        {
            var buff = text.Replace(" ", "");
            if (buff.Length % 2 != 0)
                return false;
            else
                return true;
        }

        public static string GenStartVal(int leng)
        {
            var output = "";
            for (int i = 0; i < leng; i++)
                output += "0";
            return output;
        }

        public static string ByteArrayToHexString(byte[] Bytes)
        {
            return BitConverter.ToString(Bytes).Replace("-"," ");
        }

        public static byte[] HexStringToByteArray(string HexStr)
        {
            var Hex = HexStr.Replace(" ", "");
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 
                                         0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                         0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }
        public static byte[] ConvertStringToByteArray(string text)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return enc.GetBytes(text);
        }

        public static string ConvertByteArrayToString(byte[] text)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return enc.GetString(text);
        }

        public static string ConvertStringToBinaryStr(string text)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var s = new StringBuilder();
            foreach (bool bb in new BitArray(enc.GetBytes(text)))
                s.Append(bb ? '1' : '0');
            return s.ToString();
        }

        public static string ConvertByteArraToBinaryStr(byte[] text)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var s = new StringBuilder();
            foreach (bool bb in new BitArray(text))
                s.Append(bb ? '1' : '0');
            return s.ToString();
        }

        public static string UniConvert(string text, string inflag = "Text", string outflag = "Text")
        {
            string output = "";
            byte[] buff = new byte[text.Length];
            if (inflag == outflag)
                return text;
            switch (inflag)
            {
                case "Text":
                    {
                        buff = ConvertStringToByteArray(text);
                        break;
                    }
                case "Binary":
                    {
                        buff = ConvertBinaryStrToByte(text);
                        break;
                    }
                case "Hexadecimal":
                    {
                        buff = HexStringToByteArray(text);
                        break;
                    }
            }

            switch (outflag)
            {
                case "Text":
                    {
                        output = ConvertByteArrayToString(buff);
                        break;
                    }
                case "Binary":
                    {
                        output = ConvertByteArraToBinaryStr(buff);
                        break;
                    }
                case "Hexadecimal":
                    {
                        output = ByteArrayToHexString(buff);
                        break;
                    }
            }

            return output;
        }

        public static byte[] ConvertBinaryStrToByte(string binary)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            int numOfBytes = binary.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            var buf = new BitArray(bytes);
            for(int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                    buf[i] = true;
                else
                    buf[i] = false;
            }
            buf.CopyTo(bytes, 0);
            return bytes;
        }


        public static string PadToByte(string binary)
        {
            string output = "";
            if (binary.Length < 8)
            {
                for (int i = 0; i < 8 - binary.Length; i++)
                    output += "0";
                output += binary;
                return output;
            }
            if(binary.Length % 8 != 0)
            {
                for(int i = 0; i < (binary.Length / 8 + 1) * 8 - binary.Length; i++)
                    output += "0";
                output += binary;
                return output;
            }
            return binary;
        }
        public static byte[] Encryptor(byte[] text, byte[] key)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var cipherText = new byte[text.Length];
            new BitArray(key).Xor(new BitArray(text)).CopyTo(cipherText, 0);
            return cipherText;
        }

        public static string Decryptor(byte[] text, byte[] key)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var dec = new byte[text.Length];
            new BitArray(key).Xor(new BitArray(text)).CopyTo(dec, 0);
            return enc.GetString(dec);
        }

        public static (string output, int code) Gamming(string Text, string Key, string flag = "Text")
        {
            string chiphrtext = "";
            int code = 0;
            if (flag == "Text")
            {
                if (Text.Length == Key.Length)
                {
                    var text = ConvertStringToByteArray(Text);
                    var key = ConvertStringToByteArray(Key);
                    var tt = Encryptor(text, key);
                    chiphrtext = ConvertByteArrayToString(tt);
                }
                else
                    code = 3;
            }

            if (flag == "Binary")
            {
                if (CheckIncorrectFormat(Text, "Bin") && CheckIncorrectFormat(Key, "Bin"))
                {
                    if (CheckIncorrectLength(Text) && CheckIncorrectLength(Key))
                    {
                        if (Text.Length == Key.Length)
                        {
                            var text = ConvertBinaryStrToByte(Text);
                            var key = ConvertBinaryStrToByte(Key);
                            chiphrtext = ConvertByteArraToBinaryStr(Encryptor(text, key));
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
                if (CheckIncorrectFormat(Text, "Hex") && CheckIncorrectFormat(Key, "Hex"))
                {
                    if (CheckIncorrectLength(Text) && CheckIncorrectLength(Key))
                    {
                        if (Text.Length == Key.Length)
                        {
                            var text = HexStringToByteArray(Text);
                            var key = HexStringToByteArray(Key);
                            chiphrtext = ByteArrayToHexString(Encryptor(text, key));
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
