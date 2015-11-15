using System.Collections;
using System.Web.Http;
using LicenseTest.Models;
using LicenseTest.Repositories;

namespace LicenseTest.Controllers
{
	public class LicensesController : ApiController
	{
		public Hashtable Get()
		{
			return LicenseRepository.Instance.LicenseTable;
		}

		public LicenseData Get(string domain)
		{
			LicenseData license = LicenseRepository.Instance.LicenseTable[domain] as LicenseData;

			return license;
		}
	}
}
