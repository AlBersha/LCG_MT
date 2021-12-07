using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using LCG_MT.Casino;

namespace LCG_MT
{
    static class Program
    {
        static void Main(string[] args)
        {
            #region Lcg
            
            // var nums = new List<BigInteger>();
            var nums = new List<int>();
            var lcg = new Lcg();
            
            var casino = new CasinoRoyale();
            var acc = casino.CreateAccount();
            if (acc.money > 0)
            {
                for (var i = 0; i < 3; i++)
                {
                    nums.Add((int)casino.MakeBet(1).realNumber);
                }
            }
            
            lcg.GetOdds(nums);
            Console.Out.WriteLine($"A = {lcg.A}\nC = {lcg.C}\n");
            var predictedValue = lcg.PredictValue(nums.Last());
            Console.Out.WriteLine($"Predicted value = {predictedValue}");
            
            var output = casino.MakeBet(predictedValue);
            Console.Out.WriteLine($"{output.message} - actual value = {output.realNumber}");
            #endregion
        }
    }
}