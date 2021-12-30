using System;

namespace Lab2_BCP_Feistel_network.CryptoClass
{
    public static class ScammblerClass
    {
        private static int ScramLenght = 32;
        public static uint[] LFSR_one(uint start, int format = 1) //Скремблер
        {
            uint[] output = new uint[ScramLenght];
            var ShiftRegister = start;
            if (format == 0)
                for (int i = 0; i < ScramLenght; i++)
                {
                    ShiftRegister = ((((ShiftRegister >> 2) ^ ShiftRegister) & 0x001) << 2) | (ShiftRegister >> 1);
                    output[i] = ShiftRegister | 0x01;
                }
            else
                for (int i = 0; i < ScramLenght; i++)
                {
                    ShiftRegister = ((((ShiftRegister >> 14) ^ (ShiftRegister >> 2) ^ ShiftRegister) & 0x001) << 14) | (ShiftRegister >> 1);
                    output[i] = ShiftRegister | 0x01;
                }
            return output;
        }


        public static double Hi2(string seq) //Критерий Хи квадрат
        {
            int n = seq.Length;
            int z = 0, o = 0;
            for (int i = 0; i < n; i++)
                if (seq[i] == '0')
                    z++;
                else
                    o++;
            double s = 2.0D * ((double)(n));
            s *= Math.Pow(((double)z) / ((double)n) - 0.5, 2.0) + Math.Pow(((double)o) / ((double)n) - 0.5, 2.0);
            return s;
        }

        public static (bool flag, double bal) Balance(string seq) //Тест на сбалансированность
        {
            bool flag = true;
            int interval = 1000;
            int index = 0;
            int n = seq.Length;
            var bal = 0.0;
            for (int j = 0; flag && index < n; j++)
            {
                int z = 0, o = 0;
                for (int i = j * interval; flag && i < interval * (j + 1); i++)
                {
                    index++;
                    if (seq[j] == '0')
                        z++;
                    else
                        o++;
                }
                bal = (double)Math.Abs(z - o) / interval;
                if (bal > 0.05)
                    flag = false;
            }
            return (flag, bal);
        }

        public static (bool flag, double cor) Correlation(string seq) //Тест на корреляции
        {
            bool flag = true;
            int pl = 0;
            int mi = 0;
            int n = seq.Length;
            int sdvig = 5;
            for (int i = sdvig; i < n - sdvig; i++)
            {
                if (seq[i] == seq[i + sdvig])
                    pl++;
                else
                    mi++;
            }

            if ((double)Math.Abs(pl - mi) / (n - sdvig - sdvig) > 0.05)
                flag = false;

            return (flag, (double)Math.Abs(pl - mi) / (n - sdvig - sdvig));
        }


    }
}
