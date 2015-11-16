using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LicenseEncryption
{
	public static class ObjectSerializer
	{
		public static byte[] Serialize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException();
			}

			var formatter = new BinaryFormatter();

			using (var stream = new MemoryStream())
			{
				formatter.Serialize(stream, obj);
				return stream.ToArray();
			}
		}

		public static object Deserialize(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException();
			}

			var formatter = new BinaryFormatter();

			using (var stream = new MemoryStream(data))
			{
				stream.Seek(0, SeekOrigin.Begin);
				var obj = formatter.Deserialize(stream);
				return obj;
			}
		}
	}
}