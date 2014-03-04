using System.IO;

namespace Crypto
{
    struct FileWork
    {
        internal byte[] FileToByte(string name)
        {
            try
            {
                return File.ReadAllBytes(name);
            }
            catch { return null; }
        }

        internal void WriteToFile(string name, byte[] fl)
        {
            FileStream file = null;

            try
            {
                file = new FileStream(name, FileMode.Create);
                file.Write(fl, 0, fl.Length);
            }
            catch (IOException exc)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка ввода-вывода: " + exc.Message);
            }
            catch
            {
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }
    }
}
