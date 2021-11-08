using System;
using System.Collections;
using System.Text;

namespace Lab2_BCP_Feistel_network.Utilitiets
{
    public static class ConverteUtility
    {
        private const string AllowedСharHex = "0123456789ABCDEF"; //список символов 16-ой системы

        private const string AllowedСharBin = "01"; //список символов 2-ой системы

        private static Encoding enc = Encoding.GetEncoding(1251);


        public static bool CheckIncorrectFormat(string text, string format = "Bin") // метод проверки соответствия строки формату даннных
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
            foreach (var i in buff)
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

        public static bool CheckIncorrectLength(string text) // првоерка корректноси длины строки в двоичном и 16-ном формате
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

        public static string ByteArrayToHexString(byte[] Bytes) //превращает массив байтов в 16-ую строку
        {
            return BitConverter.ToString(Bytes).Replace("-", " ");
        }

        public static byte[] HexStringToByteArray(string HexStr) //перевод 16-ой строки в массив байтов
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
        public static byte[] ConvertStringToByteArray(string text) //перевод текста в байты
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return enc.GetBytes(text);
        }

        public static string ConvertByteArrayToString(byte[] text) //перевод байтов в текст
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return enc.GetString(text);
        }

        public static string ConvertStringToBinaryStr(string text) //перевод строки в двоичное представление
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var s = new StringBuilder();
            foreach (bool bb in new BitArray(enc.GetBytes(text)))
                s.Append(bb ? '1' : '0');
            return s.ToString();
        }

        public static string ConvertByteArraToBinaryStr(byte[] text) //перевод байтов в двоичное представление
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var s = new StringBuilder();
            foreach (bool bb in new BitArray(text))
                s.Append(bb ? '1' : '0');
            return s.ToString();
        }

        public static string UniConvert(string text, string inflag = "Text", string outflag = "Text") //перевод из любого формата в любой
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

        public static byte[] ConvertBinaryStrToByte(string binary) // перевод двоичного представления в массив байтов
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            int numOfBytes = binary.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            var buf = new BitArray(bytes);
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                    buf[i] = true;
                else
                    buf[i] = false;
            }
            buf.CopyTo(bytes, 0);
            return bytes;
        }


        public static string PadToByte(string binary) //дополнение до байтов (нужен для скребмлера)
        {
            string output = "";
            if (binary.Length < 8)
            {
                for (int i = 0; i < 8 - binary.Length; i++)
                    output += "0";
                output += binary;
                return output;
            }
            if (binary.Length % 8 != 0)
            {
                for (int i = 0; i < (binary.Length / 8 + 1) * 8 - binary.Length; i++)
                    output += "0";
                output += binary;
                return output;
            }
            return binary;
        }

        public static string GetScramKey(uint[] scrambler) //получение ключа-скремблера
        {
            var s = new StringBuilder();
            foreach (var i in scrambler)
            {
                var ttt = Convert.ToInt32(i);
                var b = BitConverter.GetBytes(Convert.ToInt16(ttt));
                byte[] b1 = new byte[1] { b[0] };
                var buf = new BitArray(b1);
                var t = buf.Length - 1;
                s.Append(buf.Get(t) ? '1' : '0');
            }
            return s.ToString();
        }
    }
}
