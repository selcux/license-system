using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LicenseEncryption
{
	public class SymmetricEncryption
	{
		public byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
		{
			byte[] encryptedBytes;

			byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged aes = new RijndaelManaged())
				{
					aes.KeySize = 256;
					aes.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					aes.Key = key.GetBytes(aes.KeySize / 8);
					aes.IV = key.GetBytes(aes.BlockSize / 8);

					aes.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
						cs.Close();
					}
					encryptedBytes = ms.ToArray();
				}
			}

			return encryptedBytes;
		}

		public byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
		{
			byte[] decryptedBytes = null;

			byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged aes = new RijndaelManaged())
				{
					aes.KeySize = 256;
					aes.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					aes.Key = key.GetBytes(aes.KeySize / 8);
					aes.IV = key.GetBytes(aes.BlockSize / 8);

					aes.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
						cs.Close();
					}
					decryptedBytes = ms.ToArray();
				}
			}

			return decryptedBytes;
		}

		public string EncryptText(string input, string password)
		{
			// Get the bytes of the string
			byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			byte[] bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

			string result = Convert.ToBase64String(bytesEncrypted);

			return result;
		}

		public string DecryptText(string input, string password)
		{
			// Get the bytes of the string
			byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

			string result = Encoding.UTF8.GetString(bytesDecrypted);

			return result;
		}

		public byte[] GetRandomBytes()
		{
			int _saltSize = 4;
			byte[] ba = new byte[_saltSize];
			RNGCryptoServiceProvider.Create().GetBytes(ba);
			return ba;
		}

		public string Encrypt(string text, string pwd)
		{
			byte[] originalBytes = Encoding.UTF8.GetBytes(text);
			byte[] encryptedBytes = null;
			byte[] passwordBytes = Encoding.UTF8.GetBytes(pwd);

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			// Generating salt bytes
			byte[] saltBytes = GetRandomBytes();

			// Appending salt bytes to original bytes
			byte[] bytesToBeEncrypted = new byte[saltBytes.Length + originalBytes.Length];
			for (int i = 0; i < saltBytes.Length; i++)
			{
				bytesToBeEncrypted[i] = saltBytes[i];
			}
			for (int i = 0; i < originalBytes.Length; i++)
			{
				bytesToBeEncrypted[i + saltBytes.Length] = originalBytes[i];
			}

			encryptedBytes = Encrypt(bytesToBeEncrypted, passwordBytes);

			return Convert.ToBase64String(encryptedBytes);
		}

		public string Decrypt(string decryptedText, string pwd)
		{
			byte[] bytesToBeDecrypted = Convert.FromBase64String(decryptedText);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(pwd);

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			byte[] decryptedBytes = Decrypt(bytesToBeDecrypted, passwordBytes);

			// Getting the size of salt
			int _saltSize = 4;

			// Removing salt bytes, retrieving original bytes
			byte[] originalBytes = new byte[decryptedBytes.Length - _saltSize];
			for (int i = _saltSize; i < decryptedBytes.Length; i++)
			{
				originalBytes[i - _saltSize] = decryptedBytes[i];
			}

			return Encoding.UTF8.GetString(originalBytes);
		}
	}
}