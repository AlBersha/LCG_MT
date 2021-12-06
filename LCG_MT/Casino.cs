using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace LCG_MT
{
    public class Casino
    {
        private static string Host => "http://95.217.177.249/casino/";
        private static string CreateAccUrl => "createacc?id=";
        private string BetUrl => "play{0}?id={1}&bet={2}&number={3}";
        private string LcgMode => "Lcg";
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
                var request = WebRequest.Create(requestUrl).GetResponse();
                using var stream = new StreamReader(request.GetResponseStream()!);
                return stream.ReadToEnd();
            }
            catch (WebException e)
            {
                return string.Empty;
            }
        }

        public BetResponse MakeBet(int value)
        {
            var response = HttpGet(string.Format(Host + BetUrl, LcgMode, AccId, 10.ToString(), value.ToString()));
            return JsonSerializer.Deserialize<BetResponse>(response);
        }
    }
}