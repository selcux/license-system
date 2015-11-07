using System;

namespace LicenseServer.Models
{
	public class RequestData
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Key { get; set; }

		public string Domain { get; set; }
	}
}