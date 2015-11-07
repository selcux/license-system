using System;
using System.Windows.Forms;

namespace CryptoTest
{
	public partial class Form1 : Form
	{
		private const int keySize = 1024;
		private string publicAndPrivateKey;
		private string publicKey;

		public Form1()
		{
			InitializeComponent();

			AsymmetricEncryption.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);
		}

		private void btnEncrypt_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBox1.Text))
			{
				textBox2.Text = AsymmetricEncryption.EncryptText(textBox1.Text, keySize, publicKey);
			}
		}

		private void btnDecrypt_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBox2.Text))
			{
				textBox3.Text = AsymmetricEncryption.DecryptText(textBox2.Text, keySize, publicAndPrivateKey);
			}
		}
	}
}
