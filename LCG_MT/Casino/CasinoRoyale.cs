using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace LCG_MT.Casino
{
    public class CasinoRoyale
    {
        private static string Host => "http://95.217.177.249/casino/";
        private static string CreateAccUrl => "createacc?id=";
        private string BetUrl => "play{0}?id={1}&bet={2}&number={3}";
        private static readonly Random _random = new();
        private int AccId = _random.Next(5000);

        public CreateAccResponse CreateAccount()
        {
            var response = HttpGet(Host + CreateAccUrl + AccId);
            if (response == string.Empty)
            {
                AccId++;
                return CreateAccount();
            }
            return JsonSerializer.Deserialize<CreateAccResponse>(response);
        }

        private string HttpGet(string requestUrl)
        {
            try
            {
                using var stream = new StreamReader(WebRequest.Create(requestUrl).GetResponse().GetResponseStream()!);
                return stream.ReadToEnd();
            }
            catch (WebException)
            {
                return string.Empty;
            }
        }

        public BetResponse MakeBet(int betAmount, string mode, long value)
        {
            try
            {
                var response = HttpGet(string.Format(Host + BetUrl, mode, AccId, betAmount, value));
                return response == string.Empty ? new BetResponse() : JsonSerializer.Deserialize<BetResponse>(response);
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Invalid response was received");
                throw;
            }
        }
        
    }
}