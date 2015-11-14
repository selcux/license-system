using System;

namespace LicenseTest.Models
{
	public class ClientData
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Domain { get; set; }

		public LocalKey Key { get; set; }

		public string RemoteKey { get; set; }
	}
}