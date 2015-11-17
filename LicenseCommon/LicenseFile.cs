using System;
using System.IO;
using LicenseEncryption;

namespace LicenseCommon
{
	public static class LicenseFile
	{
		public static string FilePath { get; set; }

		public static void Write(string licenseData)
		{
			File.WriteAllText(FilePath, licenseData);
		}

		public static LicenseData Read(string key)
		{
			var encryptedStr = File.ReadAllText(FilePath);

			byte[] encryptedData = Convert.FromBase64String(encryptedStr);

			var licenseData = AsymmetricEncryption.Decrypt(encryptedData, 4096, key);
			LicenseData license = ObjectSerializer.Deserialize(licenseData) as LicenseData;

			return license;
		}
	}
}