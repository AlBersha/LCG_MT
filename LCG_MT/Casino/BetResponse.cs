using System;

namespace LCG_MT.Casino
{
    public class BetResponse
    {
        public string message { get; set; }
        public CreateAccResponse account { get; set; }
        public long realNumber { get; set; }

        public BetResponse()
        {
            message = string.Empty;
        }
    }
}