using System;
using System.Linq;
using System.Web.Mvc;
using LicenseEncryption;
using LicenseTest.Models;
using LicenseTest.Repositories;

namespace LicenseTest.Controllers
{
	public class RegisterController : Controller
	{
		// GET: Register
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Register(Guid id)
		{
			var data = DataRepository.Instance.ClienDataList.SingleOrDefault(clientData => clientData.Id == id);
			return View(data);
		}

		[HttpPost]
		public ActionResult Register(RegisterData data)
		{
			//			SymmetricEncryption se = new SymmetricEncryption();
			DataRepository.Instance.RegisterList.Add(data);

			string publicKey;
			string privateKey;
			AsymmetricEncryption.GenerateKeys(4096, out publicKey, out privateKey);

			ClientData clientData = new ClientData
			{
				Domain = data.Domain,
				Key = new LocalKey
				{
					PrivateKey = privateKey,
					PublicKey = publicKey
				}
			};

			DataRepository.Instance.ClienDataList.Add(clientData);

			return View(clientData);
		}

		public ActionResult List()
		{
			return View(DataRepository.Instance.ClienDataList);
		}
	}
}