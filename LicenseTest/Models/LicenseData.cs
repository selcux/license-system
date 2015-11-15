using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenseTest.Models
{
	public class LicenseData
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public DateTime StartDate { get; set; } = DateTime.UtcNow;

		public uint LicensePeriodInDays = 365;

		[NotMapped]
		public DateTime ExpiryDate => StartDate.AddDays(LicensePeriodInDays);
	}
}