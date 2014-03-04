namespace Crypto
{
    class E32 : Converter
    {
        byte[] encrByteFile;
        uint[] uintKey;
        ulong[] ulongFile;

        private E32() { }

        internal E32(byte[] file, byte[] key)
        {
            uintKey = GetUIntKeyArray(key);
            ulongFile = GetULongDataArray(file);

            encrByteFile = ConvertToByte(EncryptFile());
        }

        internal byte[] GetEncryptFile
        {
            get { return encrByteFile; }
        }

        private ulong[] EncryptFile()
        {
            BasicStep[] K = new BasicStep[8];
            ulong[] ulongEncrFile = new ulong[ulongFile.Length];
            
            for (int k = 0; k < ulongFile.Length; k++)
            {
                ulongEncrFile[k] = ulongFile[k];

                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < K.Length; i++)
                    {
                        K[i] = new BasicStep(ulongEncrFile[k], uintKey[i]);
                        ulongEncrFile[k] = K[i].BasicEncrypt(false);
                    }
                }

                for (int i = K.Length - 1; i >= 0; i--)
                {
                    K[i] = new BasicStep(ulongEncrFile[k], uintKey[i]);

                    if (i != 0)
                        ulongEncrFile[k] = K[i].BasicEncrypt(false);
                    else
                        ulongEncrFile[k] = K[i].BasicEncrypt(true);
                }
            }

            return ulongEncrFile;
        }
    }
}