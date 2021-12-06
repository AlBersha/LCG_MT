using System;
using System.Collections.Generic;
using System.Numerics;

namespace LCG_MT
{
    public class Lcg
    {
        public BigInteger A { get; set; }
        public BigInteger C { get; set; }

        private static BigInteger modulus => (BigInteger)Math.Pow(2, 32);

        public void GetOdds(List<BigInteger> numbers)
        {
            var n21 = numbers[2] - numbers[1];
            var n10 = numbers[1] - numbers[0];

            A = n21 / BigInteger.Remainder(n10, modulus);
            C = BigInteger.Remainder(numbers[1] - A * numbers[0], modulus);
        }

        public BigInteger PredictValue(BigInteger input) => BigInteger.Remainder(A * input + C, modulus);
    }
}