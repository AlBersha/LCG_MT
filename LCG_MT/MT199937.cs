using System.Collections.Generic;
using System.Linq;

namespace LCG_MT
{
    public class MT199937
    {
        private const ulong N = 624;
        private const ulong M = 397;
        private static readonly ulong[] Magic = { 0x0, 0x9908b0df };
        private const ulong UpperMask = 0x80000000UL;
        private const ulong LowerMask = 0x7fffffffUL;
        private const int Factor1 = 1812433253;
        private const ulong Mask1   = 0x9d2c5680UL;
        private const ulong Mask2   = 0xefc60000UL;

        private const int MersU = 11;
        private const int MersS = 7;
        private const int MersT = 15;
        private const int MersL = 18;
        private const int MersF = 14;

        private ulong[] _mt;
        private ulong _mti;

        public MT199937() { }

        public MT199937(ulong seed) {
            _mt ??= new ulong[N];
            _mt[0] = seed;
            for (_mti = 1; _mti < N; _mti++) {
                _mt[_mti] = (Factor1 * (_mt[_mti-1] ^ (_mt[_mti-1] >> 30)) + _mti);
                _mt[_mti] &= 0xffffffffUL;
            }
        }
        
        public ulong PredictValue() {
            ulong y, coef;
            if (_mti >= N) {
                for (coef = 0; coef < N-M; coef++) {
                    y = (_mt[coef] & UpperMask) | (_mt[coef+1] & LowerMask);
                    _mt[coef] = _mt[coef+M] ^ (y >> 1) ^ Magic[y & 0x1UL];
                }
                for (;coef < N-1; coef++) {
                    y = (_mt[coef] & UpperMask) | (_mt[coef+1] & LowerMask);
                    _mt[coef] = _mt[coef-227] ^ (y >> 1) ^ Magic[y & 0x1];
                }
                y = (_mt[N-1] & UpperMask) | (_mt[0] & LowerMask);
                _mt[N-1] = _mt[M-1] ^ (y >> 1) ^ Magic[y & 0x1];

                _mti = 0;
            }

            y = _mt[_mti++];

            return Temper(y);
        }

        public ulong BetterMtPredictValue(List<ulong> states)
        {
            var unTemperedStates = states.Select(state => UnTemper(state)).ToList();
            _mt = unTemperedStates.ToArray();
            return PredictValue();
        }

        private ulong Temper(ulong number)
        {
            number ^= number >> MersU;
            number ^= number << MersS & Mask1;
            number ^= number << MersT & Mask2;
            number ^= number >> MersL;
            
            return number;
        }

        private ulong UnTemper(ulong number)
        { 
            number ^= number >> MersL;
            number ^= number << MersT & 0xefc60000UL;
            number ^=
                number << MersS & 0x9d2c5680UL ^
                number << MersF & 0x94284000UL ^
                number << 21 & 0x14200000UL ^
                number << 28 & 0x10000000UL;
            number ^= number >> 11 ^ number >> 22;

            return number;
            
        }
    }
}