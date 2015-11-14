using System;

namespace ClientTest.Models
{
	public class LocalKey
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string PrivateKey { get; set; }

		public string PublicKey { get; set; }
	}
}