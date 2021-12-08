using System;
using System.Collections.Generic;
using System.Linq;
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

            // #region Lcg
            //
            // var nums = new List<int>();
            // var lcg = new Lcg();
            // var lcgMode = "Lcg";
            //
            // if (acc.money > 0)
            // {
            //     for (var i = 0; i < 3; i++)
            //     {
            //         nums.Add((int)casino.MakeBet(1, lcgMode, 1).realNumber);
            //     }
            // }
            //
            // lcg.GetOdds(nums);
            // Console.Out.WriteLine($"A = {lcg.A}\nC = {lcg.C}\n");
            // var predictedValueLcg = lcg.PredictValue(nums.Last());
            // Console.Out.WriteLine($"Predicted value = {predictedValueLcg}");
            //
            // var output = casino.MakeBet(1, lcgMode, (long)predictedValueLcg);
            // while (output.message is "" or "You lost this time")
            // {
            //     Console.Out.WriteLine(output.message);
            //     nums.Remove(0);
            //     nums.Add((int)output.realNumber);
            //     predictedValueLcg = lcg.PredictValue(nums.Last());
            //     output = casino.MakeBet(1, lcgMode, predictedValueLcg);
            // }
            // Console.Out.WriteLine(output.message);
            // #endregion
            //
            #region MT199937
            
            acc = casino.CreateAccount();
            const string MTmode = "Mt";
            var seed = DateTimeOffset.Parse(acc.deletionTime).ToUnixTimeSeconds() - 3600;
            
            var mtGenerator = new MT199937((ulong)seed);
            var predictedValue = mtGenerator.PredictValue();
            
            var bet = casino.MakeBet(1, MTmode, (long)predictedValue).realNumber;
            
            while (predictedValue != (ulong)bet)
            {
                seed++;
                mtGenerator = new MT199937((ulong)seed);
                predictedValue = mtGenerator.PredictValue();
            }
            
            casino.MakeBet(900, MTmode, (long)mtGenerator.PredictValue());
            var response = casino.MakeBet(1000, MTmode, (long)mtGenerator.PredictValue());
            Console.Out.WriteLine($"\n{response.message} \nmoney amount - {response.account.money}");
            
            #endregion

            #region BetterMt
            
            var BetterMtMode = "BetterMt";
            var states = new List<ulong>();
            for (var i = 0; i < 624; i++)
            {
                states.Add((ulong)casino.MakeBet(1, BetterMtMode, 0).realNumber);
                Console.Out.WriteLine(i);
            }
            
            var res = new MT199937().BetterMtPredictValue(states);
            Console.Out.WriteLine(casino.MakeBet(1000, BetterMtMode, (long)res).message);
            #endregion
        }
    }
}