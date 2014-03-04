using System;

namespace Crypto
{
    struct BasicStep
    {
        uint N1, N2, X;

        internal BasicStep(ulong dateFragment, uint keyFragment)
        {
            N1 = (uint)(dateFragment >> 32);
            N2 = (uint)((dateFragment << 32) >> 32);
            X = keyFragment;
        }

        internal ulong BasicEncrypt(bool IsLastStep)
        {
            return (FourthAndFifthStep(IsLastStep, ThirdStep(SecondStep(FirstStep()))));
        }

        private uint FirstStep()
        {
            return (uint)((X + N1) % (Convert.ToUInt64(Math.Pow(2, 32))));
        }

        private uint SecondStep(uint S)
        {
            uint newS, S0, S1, S2, S3, S4, S5, S6, S7;

            S0 = S >> 28;
            S1 = (S << 4) >> 28;
            S2 = (S << 8) >> 28;
            S3 = (S << 12) >> 28;
            S4 = (S << 16) >> 28;
            S5 = (S << 20) >> 28;
            S6 = (S << 24) >> 28;
            S7 = (S << 28) >> 28;

            S0 = ReplacementTab.Table0[S0];
            S1 = ReplacementTab.Table0[0x10 + S1];
            S2 = ReplacementTab.Table0[0x20 + S2];
            S3 = ReplacementTab.Table0[0x30 + S3];
            S4 = ReplacementTab.Table0[0x40 + S4];
            S5 = ReplacementTab.Table0[0x50 + S5];
            S6 = ReplacementTab.Table0[0x60 + S6];
            S7 = ReplacementTab.Table0[0x70 + S7];

            newS = S7 + (S6 << 4) + (S5 << 8) + (S4 << 12) + (S3 << 16) +
                    (S2 << 20) + (S1 << 24) + (S0 << 28);

            return newS;
        }

        private uint ThirdStep(uint S)
        {
            return (uint)(S << 11) | (S >> 21);
        }

        private ulong FourthAndFifthStep(bool IsLastStep, uint S)
        {
            ulong N;

            S = (S ^ N2);

            if (!IsLastStep)
            {
                N2 = N1;
                N1 = S;
            }
            else
                N2 = S;

            N = ((ulong)N2) | (((ulong)N1) << 32);

            return N;
        }
    }
}