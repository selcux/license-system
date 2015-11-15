using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LicenseEncryption;
using LicenseTest.Repositories;

namespace LicenseTest.Controllers
{
	public class ClientConnectionController : ApiController
	{
		public IHttpActionResult Post(Dictionary<string, string> pData)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			SymmetricEncryption se = new SymmetricEncryption();
			string key = pData["Key"];
			string domain = pData["Domain"];
			string remoteKey = se.Decrypt(key, domain);

			var clientData = DataRepository.Instance.ClienDataList.FirstOrDefault(data => data.Domain.Equals(domain));

			if (clientData == null)
			{
				return NotFound();
			}

			clientData.RemoteKey = remoteKey;

			LicenseRepository.Instance.GenerateLicense(domain, 365);

			return Ok(remoteKey);
		}

	}
}
