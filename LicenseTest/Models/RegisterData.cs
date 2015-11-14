using System;

namespace LicenseTest.Models
{
	public class RegisterData
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Domain { get; set; }

		//public string Key { get; set; }
	}
}