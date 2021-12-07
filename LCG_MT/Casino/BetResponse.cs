using System;

namespace LCG_MT.Casino
{
    public class BetResponse
    {
        public string message { get; set; }
        public CreateAccResponse account { get; set; }
        public int realNumber { get; set; }
    }
}