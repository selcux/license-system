using System;
using System.Linq;
using System.Web.Http;
using LicenseEncryption;
using LicenseTest.Repositories;

namespace LicenseTest.Controllers
{
	public class ClientConnectionController : ApiController
	{
		// TODO: Client public key will be received and license data will be sent
		public IHttpActionResult Post(Tuple<string, string> pData)
		{
			SymmetricEncryption se = new SymmetricEncryption();
			string key = pData.Item1;
			string domain = pData.Item2;
			string remoteKey = se.Decrypt(key, domain);

			var clientData = DataRepository.Instance.ClienDataList.FirstOrDefault(data => data.Domain.Equals(domain));

			if (clientData == null)
			{
				return NotFound();
			}

			clientData.RemoteKey = remoteKey;

			return Ok(remoteKey);
		}
	}
}
