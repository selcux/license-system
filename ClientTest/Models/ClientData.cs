using System;

namespace ClientTest.Models
{
	public class ClientData
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public LocalKey Key { get; set; }
	}
}