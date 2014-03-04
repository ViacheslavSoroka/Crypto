using System;

namespace Crypto
{
    class Converter
    {
        internal byte[] ConvertToByte(ulong[] fl)
        {
            byte[] temp = new byte[8];
            byte[] encrByteFile = new byte[fl.Length * 8];

            for (int i = 0; i < fl.Length; i++)
            {
                temp = BitConverter.GetBytes(fl[i]);

                for (int j = 0; j < temp.Length; j++)
                    encrByteFile[j + i * 8] = temp[j];
            }

            return encrByteFile;
        }

        internal byte[] ConvertToByte(uint[] fl)
        {
            byte[] temp = new byte[4];
            byte[] encrByteFile = new byte[fl.Length * 4];

            for (int i = 0; i < fl.Length; i++)
            {
                temp = BitConverter.GetBytes(fl[i]);

                for (int j = 0; j < temp.Length; j++)
                    encrByteFile[j + i * 4] = temp[j];
            }

            return encrByteFile;
        }

        internal uint[] GetUIntKeyArray(byte[] byteKey)
        {
            uint[] key = new uint[byteKey.Length / 4];

            for (int i = 0; i < key.Length; i++)
            {
                key[i] = BitConverter.ToUInt32(byteKey, i * 4);
            }

            return key;
        }

        internal ulong[] GetULongDataArray(byte[] byteData)
        {
            ulong[] data = new ulong[byteData.Length / 8];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = BitConverter.ToUInt64(byteData, i * 8);
            }

            return data;
        }
    }
}