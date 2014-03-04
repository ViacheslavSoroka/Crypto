using System;
using System.Windows.Forms;

namespace Crypto
{
    internal partial class KeyGenerator : Form
    {
        Timer timer;
        Random rand;
        string strKey, fileName;
        int count;

        internal enum Type { Key, S }

        internal KeyGenerator(Type type)
        {
            InitializeComponent();

            if (type == Type.Key)
            {
                count = 256;
                fileName = "Key";
            }
            else
            {
                count = 64;
                fileName = "S";
            }

            rand = new Random();

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (((PointToClient(Cursor.Position).X + rand.Next(20))% 2 == 0) && 
                ((PointToClient(Cursor.Position).Y + rand.Next(20)) % 2 != 0))
                strKey += "0";
            else if (((PointToClient(Cursor.Position).Y + rand.Next(20)) % 2 == 0) && 
                ((PointToClient(Cursor.Position).X + rand.Next(20)) % 2 != 0))
                strKey += "1";

            if (strKey.Length == count)
            {
                timer.Enabled = false;
                CreateKey();
            }
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled)
            {
                timer.Enabled = true;
                strKey = "";
            }
        }

        private void CreateKey()
        {
            byte[] key = new byte[strKey.Length / 8];
            string[] keyParts = new string[strKey.Length / 8];
            int count = 0;

            for (int i = 0; i < strKey.Length; i++)
            {
                if ((i % 8 == 0) && (i != 0))
                    count++;

                keyParts[count] += strKey[i];
            }

            for (int i = 0; i < key.Length; i++)
            {
                key[i] = Convert.ToByte(keyParts[i], 2);
            }

            FileWork fw = new FileWork();
            
            fw.WriteToFile(fileName, key);
            MessageBox.Show("Ключ создан!");

            this.Close();
        }

        private void KeyGenerator_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Подвигайте мышкой случайным образом. Это поможет создать качественный ключ.");
        }
    }
}

