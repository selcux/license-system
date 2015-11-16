using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LicenseCommon;
using LicenseEncryption;
using LicenseTest.Repositories;

namespace LicenseTest.Controllers
{
	public class LicensesController : ApiController
	{
		public Hashtable Get()
		{
			return LicenseRepository.Instance.LicenseTable;
		}

		public IHttpActionResult Get(string domain)
		{
			if (string.IsNullOrEmpty(domain))
			{
				return BadRequest();
			}

			LicenseData license = LicenseRepository.Instance.LicenseTable[domain] as LicenseData;

			if (license == null)
			{
				return NotFound();
			}

			var clientData = DataRepository.Instance.ClienDataList.SingleOrDefault(data => data.Domain == domain);

			if (clientData == null)
			{
				return NotFound();
			}

			var publicKey = clientData.RemoteKey;
//			var licenseData = Encoding.UTF8.GetString(ObjectSerializer.Serialize(license));

//			byte[] encryptedLicense = AsymmetricEncryption.Encrypt(ObjectSerializer.Serialize(license), 1024, clientData.Key.PublicKey);
			var encryptedLicense = AsymmetricEncryption.Encrypt(ObjectSerializer.Serialize(license), 4096, publicKey);

			string encryptedLicenseStr = Convert.ToBase64String(encryptedLicense);
			Dictionary<string, string> dataDict = new Dictionary<string, string> {{"data", encryptedLicenseStr}};

			return Ok(dataDict);
		}
	}
}
