using System;
using System.Collections.Generic;
using System.Numerics;

namespace LCG_MT
{
    public class Lcg
    {
        public long A { get; private set; }
        public long C { get; private set; }
        private static long Modulus => (long)Math.Pow(2, 32);

        public void GetOdds(List<int> numbers)
        {
            var n21 = (long)numbers[2] - numbers[1];
            var n10 = (long)numbers[1] - numbers[0];

            A = GetModulus(n21 * ModInverse(n10));
            C = (numbers[1] - A * numbers[0]) % Modulus;

        }

        public long PredictValue(int input) => (A * input + C) % Modulus;

        private long GetModulus(long input)
        {
            return input - (input / Modulus) * Modulus;
        }
        private long ModInverse(long num)
        {
            if (Modulus == 1) return 0;
            
            var modulus = Modulus;
            var (x, y) = ((long)1, (long)0);

            while (num > 1)
            {
                var q = num / modulus;
                (num, modulus) = (modulus, num % modulus);
                (x, y) = (y, x - q * y);
            }

            return x < 0 ? x + Modulus : x;
        }
    }
}