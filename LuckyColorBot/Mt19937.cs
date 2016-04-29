using System.Linq;

namespace LuckyColorBot
{
    public class Mt19937
    {
        private const int NumberN = 624;
        private const int NumberM = 397;
        private const uint MatrixA = 0x9908b0dfU;
        private const uint UpperMask = 0x80000000U;
        private const uint LowerMask = 0x7fffffffU;
        private readonly uint[] mt = new uint[NumberN];
        private int mti = NumberN + 1;

        public Mt19937()
        {
        }

        public Mt19937(uint s)
        {
            InitGet(s);
        }

        public Mt19937(uint[] initKey)
        {
            InitByArray(initKey);
        }

        public void InitGet(uint s)
        {
            mt[0] = s & 0xffffffffU;
            for (mti = 1; mti < NumberN; mti++)
            {
                mt[mti] = 1812433253U*(mt[mti - 1] ^ (mt[mti - 1] >> 30)) + (uint) mti;
                mt[mti] &= 0xffffffffU;
            }
        }

        public void InitByArray(uint[] initKey)
        {
            int i, j, k;
            var keyLength = initKey.Count();
            InitGet(19650218U);
            i = 1;
            j = 0;
            k = NumberN > keyLength ? NumberN : keyLength;
            for (; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30))*1664525U)) + initKey[j] + (uint) j;
                mt[i] &= 0xffffffffU;
                i++;
                j++;
                if (i >= NumberN)
                {
                    mt[0] = mt[NumberN - 1];
                    i = 1;
                }
                if (j >= keyLength) j = 0;
            }
            for (k = NumberN - 1; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30))*1566083941U)) - (uint) i;
                mt[i] &= 0xffffffffU;
                i++;
                if (i >= NumberN)
                {
                    mt[0] = mt[NumberN - 1];
                    i = 1;
                }
            }

            mt[0] = 0x80000000U;
        }

        public uint GetInt32()
        {
            uint y;
            var mag01 = new uint[2] {0x0U, MatrixA};

            if (mti >= NumberN)
            {
                int kk;

                if (mti == NumberN + 1)
                    InitGet(5489U);

                for (kk = 0; kk < NumberN - NumberM; kk++)
                {
                    y = (mt[kk] & UpperMask) | (mt[kk + 1] & LowerMask);
                    mt[kk] = mt[kk + NumberM] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                for (; kk < NumberN - 1; kk++)
                {
                    y = (mt[kk] & UpperMask) | (mt[kk + 1] & LowerMask);
                    mt[kk] = mt[kk + (NumberM - NumberN)] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                y = (mt[NumberN - 1] & UpperMask) | (mt[0] & LowerMask);
                mt[NumberN - 1] = mt[NumberM - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

                mti = 0;
            }

            y = mt[mti++];

            y ^= y >> 11;
            y ^= (y << 7) & 0x9d2c5680U;
            y ^= (y << 15) & 0xefc60000U;
            y ^= y >> 18;

            return y;
        }

        public uint GetInt31()
        {
            return GetInt32() >> 1;
        }

        public double GetReal1()
        {
            return GetInt32()*(1.0/4294967295.0);
        }

        public double GetReal2()
        {
            return GetInt32()*(1.0/4294967296.0);
        }

        public double GetReal3()
        {
            return (GetInt32() + 0.5)*(1.0/4294967296.0);
        }

        public double GetReal53()
        {
            uint a = GetInt32() >> 5, b = GetInt32() >> 6;
            return (a*67108864.0 + b)*(1.0/9007199254740992.0);
        }
    }
}