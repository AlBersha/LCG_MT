using System;
using System.Collections.Generic;
using System.Numerics;

namespace LCG_MT
{
    public class Lcg
    {
        public BigInteger A { get; private set; }
        public BigInteger C { get; private set; }
        private static BigInteger Modulus => (BigInteger)Math.Pow(2, 32);

        public void GetOdds(List<int> numbers)
        {
            var n21 = (BigInteger)numbers[2] - numbers[1];
            var n10 = (BigInteger)numbers[1] - numbers[0];

            A = GetModulus(n21 * ModInverse(n10));
            C = (numbers[1] - A * numbers[0]) % Modulus;

        }

        public BigInteger PredictValue(int input) => (A * input + C) % Modulus;

        private BigInteger GetModulus(BigInteger input)
        {
            return input - (input / Modulus) * Modulus;
        }
        private BigInteger ModInverse(BigInteger num)
        {
            if (Modulus == 1) return 0;
            
            var modulus = Modulus;
            var (x, y) = (BigInteger.One, BigInteger.Zero);

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