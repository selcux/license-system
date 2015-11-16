using System;

namespace LicenseCommon
{
	[Serializable]
	public class LicenseData
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public DateTime StartDate { get; set; } = DateTime.UtcNow;

		public uint LicensePeriodInDays = 365;

		public DateTime ExpiryDate => StartDate.AddDays(LicensePeriodInDays);
	}
}