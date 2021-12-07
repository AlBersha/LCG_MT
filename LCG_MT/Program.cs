using System;
using LCG_MT.Casino;
using MersenneTwister;

namespace LCG_MT
{
    static class Program
    {
        static void Main(string[] args)
        {
            var casino = new CasinoRoyale();
            var acc = casino.CreateAccount();

            #region Lcg
            
            // var nums = new List<int>();
            // var lcg = new Lcg();
            // var lcgMode = "Lcg";
            //
            // if (acc.money > 0)
            // {
            //     for (var i = 0; i < 3; i++)
            //     {
            //         nums.Add(casino.MakeBet(lcgMode, 1).realNumber);
            //     }
            // }
            //
            // lcg.GetOdds(nums);
            // Console.Out.WriteLine($"A = {lcg.A}\nC = {lcg.C}\n");
            // var predictedValue = lcg.PredictValue(nums.Last());
            // Console.Out.WriteLine($"Predicted value = {predictedValue}");
            //
            // var output = casino.MakeBet(lcgMode, predictedValue);
            // Console.Out.WriteLine(output.message);
            #endregion

            #region MT199937

            const string MTmode = "Mt";
            var seed = DateTimeOffset.Parse(acc.deletionTime).ToUnixTimeMilliseconds();

            var MTrng = new MT199937((ulong)seed);
            var predictedValue = MTrng.PredictValue(32);
            
            var bet = casino.MakeBet(MTmode, predictedValue).realNumber;

            while (predictedValue != (ulong)bet)
            {
                seed++;
                predictedValue = MTrng.PredictValue(4);
            }
            var mt = MTRandom.Create(MTEdition.Original_19937);
            var r = mt.Next();

            #endregion
        }
    }
}