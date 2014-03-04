using System;

namespace Crypto
{
    class Gamma : Converter
    {
        private byte[] SWork;
        byte[] file;

        internal Gamma(byte[] fl, byte[] key, byte[] S)
        {
            E32 e32 = new E32(S, key);
            SWork = e32.GetEncryptFile;
            file = new byte[fl.Length];

            for (int i = 0; i < fl.Length; i++)
                file[i] = fl[i];
        }

        internal byte[] StartGamma()
        {
            byte[] crFile = new byte[file.Length];
            uint[] tempS = new uint[2];
            uint S0, S1;
            int count1, count2;

            if (file.Length % 8 == 0)
                count1 = file.Length / 8;
            else
                count1 = file.Length / 8 + 1;

            count2 = 8;

            for (int i = 0; i < count1; i++)
            {               
                if(i == (count1 - 1))
                    count2 = file.Length % 8;

                for (int j = 0; j < count2; j++)
                {
                    tempS = GetUIntKeyArray(SWork);
                    S0 = tempS[0];
                    S1 = tempS[1];

                    S0 = (uint)((S0 + 0x1010101) % (Convert.ToUInt64(Math.Pow(2, 32))));
                    S1 = (uint)(((S1 + 0x1010104 - 1) % (Convert.ToUInt64(Math.Pow(2, 32) - 1))) + 1);

                    tempS[0] = S0;
                    tempS[1] = S1;

                    SWork = ConvertToByte(tempS);

                    crFile[j + i * 8] = (byte)(file[j + i * 8] ^ SWork[j]);
                }
            }

            return crFile;
        }
    }
}