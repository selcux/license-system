using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace LicenseServer.Providers
{
	public class LicenseProvider
	{
		public Tuple<byte[], byte[]> Encrypt(string publicKey, string dataStr)
		{
			byte[] publicKeyData = System.Text.Encoding.UTF8.GetBytes(publicKey);
			byte[] exponent = { 1, 0, 1 };

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

			RSAParameters rsaKeyInfo = new RSAParameters
			{
				Modulus = publicKeyData,
				Exponent = exponent
			};

			rsa.ImportParameters(rsaKeyInfo);

			RijndaelManaged rm = new RijndaelManaged();

			var encryptedSymmetricKey = rsa.Encrypt(rm.Key, false);
			var encryptedSymmetricIv = rsa.Encrypt(rm.IV, false);

/*
			MemoryStream ms = new MemoryStream();
			using (BufferedStream bs = new BufferedStream(ms))
			{
				using (CryptoStream cryptoStream = new CryptoStream(bs,
					rm.CreateEncryptor(encryptedSymmetricKey, encryptedSymmetricIv),
				CryptoStreamMode.Write))
				{
					using (StreamWriter sw = new StreamWriter(cryptoStream))
					{
						sw.Write(dataStr);
						
					}
				}
			}
*/

			return Tuple.Create(encryptedSymmetricKey, encryptedSymmetricIv);
		}

		public void Decrypt(byte[] encryptedSymmetricKey, byte[] encryptedSymmetricIv)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

			RijndaelManaged rmCrypto = new RijndaelManaged();

			byte[] symmetricKey = rsa.Decrypt(encryptedSymmetricKey, false);
			byte[] symmetricIV = rsa.Decrypt(encryptedSymmetricIv, false);

			rmCrypto.CreateDecryptor(symmetricKey, symmetricIV);

		}

		public MemoryStream SerializeObject(object data)
		{
			MemoryStream ms = new MemoryStream();
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(ms, data);
			return ms;
		}

		public object DeserializeObject(MemoryStream ms)
		{
			IFormatter formatter = new BinaryFormatter();
			ms.Seek(0, SeekOrigin.Begin);
			object data = formatter.Deserialize(ms);
			return data;
		}
	}
}