using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Gamming_Srammbling.CryptoClass
{
    public static class ScammblerClass
    {
        public static ulong[] LFSR_one(string text, ulong start)
        {
            ulong[] output = new ulong[text.Length];
            var ShiftRegister = start;
            for (int i = 0; i < text.Length; i++)
            {                
                ShiftRegister = ((((ShiftRegister >> 9) ^ (ShiftRegister >> 3) ^ ShiftRegister) & 0x01) << 9) | (ShiftRegister >> 1);
                output[i] = ShiftRegister & 0x01;
            }
            return output;
        }
    }
}
