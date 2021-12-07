using System.Collections.Generic;

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

        private ulong[] _mt;
        private ulong _mti;

        public MT199937(ulong seed) {
            _mt ??= new ulong[N];
            _mt[0] = seed;
            for (_mti = 1; _mti < N; _mti++) {
                _mt[_mti] = (Factor1 * (_mt[_mti-1] ^ (_mt[_mti-1] >> 30)) + _mti);
                _mt[_mti] &= 0xffffffffUL;
            }
        }
        
        public ulong PredictValue() {
            ulong y, kk;
            if (_mti >= N) {
                for (kk = 0; kk < N-M; kk++) {
                    y = (_mt[kk] & UpperMask) | (_mt[kk+1] & LowerMask);
                    _mt[kk] = _mt[kk+M] ^ (y >> 1) ^ Magic[y & 0x1UL];
                }
                for (;kk < N-1; kk++) {
                    y = (_mt[kk] & UpperMask) | (_mt[kk+1] & LowerMask);
                    _mt[kk] = _mt[kk-227] ^ (y >> 1) ^ Magic[y & 0x1];
                }
                y = (_mt[N-1] & UpperMask) | (_mt[0] & LowerMask);
                _mt[N-1] = _mt[M-1] ^ (y >> 1) ^ Magic[y & 0x1];

                _mti = 0;
            }

            y = _mt[_mti++];

            y ^= (y >> MersU);
            y ^= (y << MersS) & Mask1;
            y ^= (y << MersT) & Mask2;
            y ^= (y >> MersL);

            return y;
        }
    }
}