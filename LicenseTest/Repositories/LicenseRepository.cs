using System;
using System.Collections;
using System.Collections.Generic;
using LicenseTest.Models;

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

			LicenseTable.Add(domain, license);

			return license;
		}
	}
}