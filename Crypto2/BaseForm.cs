using System;
using System.Text;
using System.Windows.Forms;

namespace Crypto
{
    public partial class BaseForm : Form
    {
        byte[] encrByteFile, byteKey, byteS, decrByteFile;

        public BaseForm()
        {
            InitializeComponent();
        }

        private void fileEncryptButton_Click(object sender, EventArgs e)
        {
            Gamma gam;

            if (decrTextBox.Text == "")
                MessageBox.Show("Введите данные для шифрования.");
            else
            {
                byte[] btFile = Encoding.Default.GetBytes(decrTextBox.Text);

                if ((byteKey == null) || (byteKey.Length != 32))
                    MessageBox.Show("Введдите 256-битный ключ.");
                else if ((byteS == null) || (byteS.Length != 8))
                    MessageBox.Show("Введдите 64-битную синхропосылку.");
                else
                {
                    gam = new Gamma(btFile, byteKey, byteS);
                    encrByteFile = gam.StartGamma();
                    encrTextBox.Text = Encoding.Default.GetString(encrByteFile);
                }
            }            
        }

        private void fileDecryptButton_Click(object sender, EventArgs e)
        {
            Gamma gam;

            if ((encrTextBox.Text == "") && (decrTextBox.Text == ""))
                MessageBox.Show("Введите данные для шифрования.");
            else
            {
                byte[] btFile = encrByteFile;

                if (btFile != null)
                {

                    if ((byteKey == null) || (byteKey.Length != 32))
                        MessageBox.Show("Введдите 256-битный ключ.");
                    else if ((byteS == null) || (byteS.Length != 8))
                        MessageBox.Show("Введдите 64-битную синхропосылку.");
                    else
                    {
                        gam = new Gamma(btFile, byteKey, byteS);
                        decrByteFile = gam.StartGamma();
                        encrTextBox.Text = Encoding.Default.GetString(decrByteFile);
                    }
                }
            }
        }

        private void loadKeyButton_Click(object sender, EventArgs e)
        {
            FileWork fw = new FileWork();

            openFileDialog1.ShowDialog();
            string key = openFileDialog1.FileName;
            byteKey =  fw.FileToByte(key);

            if(byteKey != null)
                keyTextBox.Text = Encoding.Default.GetString(byteKey);
        }

        private void fileLoadButton_Click(object sender, EventArgs e)
        {
            FileWork fw = new FileWork();

            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            decrByteFile = fw.FileToByte(file);
            
            if(decrByteFile != null)
                decrTextBox.Text = Encoding.Default.GetString(decrByteFile);
        }

        private void fileSaveButton_Click(object sender, EventArgs e)
        {
            FileWork fw = new FileWork();

            saveFileDialog1.ShowDialog();
            string file = saveFileDialog1.FileName;
            fw.WriteToFile(file, encrByteFile);
        }

        private void keyGenerateButton_Click(object sender, EventArgs e)
        {
            KeyGenerator kg = new KeyGenerator(KeyGenerator.Type.Key);
            kg.ShowDialog();
        }

        private void sLoadButton_Click(object sender, EventArgs e)
        {
            FileWork fw = new FileWork();

            openFileDialog1.ShowDialog();
            string S = openFileDialog1.FileName;
            byteS = fw.FileToByte(S);

            if (byteS != null)
                sTextBox.Text = Encoding.Default.GetString(byteS);
        }

        private void SGenerateButton_Click(object sender, EventArgs e)
        {
            KeyGenerator kg = new KeyGenerator(KeyGenerator.Type.S);
            kg.ShowDialog();
        }
    }
}
