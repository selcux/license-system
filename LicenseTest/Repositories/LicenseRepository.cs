using System;
using System.Collections;
using LicenseCommon;

namespace LicenseTest.Repositories
{
	public class LicenseRepository
	{
		#region Singleton

		private static readonly Lazy<LicenseRepository> instance = new Lazy<LicenseRepository>(() => new LicenseRepository());

		public static LicenseRepository Instance => instance.Value;

		private LicenseRepository()
		{
		}

		#endregion

//		public Dictionary<string, LicenseData> LicenseTable { get; } = new Dictionary<string, LicenseData>();
		public Hashtable LicenseTable { get; } = new Hashtable();

		public LicenseData GenerateLicense(string domain, uint days)
		{
			LicenseData license = new LicenseData
			{
				LicensePeriodInDays = days
			};

			LicenseTable[domain] = license;

			return license;
		}
	}
}