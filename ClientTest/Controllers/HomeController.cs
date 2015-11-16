using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClientTest.Models;
using ClientTest.Repositories;
using LicenseCommon;
using LicenseEncryption;

namespace ClientTest.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		// GET: Home/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(DomainData data)
		{
			SymmetricEncryption se = new SymmetricEncryption();

			DataRepository.Instance.Domain = data.Domain;
			DataRepository.Instance.RemoteKey = se.Decrypt(data.Key, data.Domain);

			string privateKey;
			string publicKey;
			AsymmetricEncryption.GenerateKeys(4096, out publicKey, out privateKey);

			DataRepository.Instance.Key = new LocalKey
			{
				PrivateKey = privateKey,
				PublicKey = publicKey
			};

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:4427/");
//				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				string encryptedPublicKey = se.Encrypt(DataRepository.Instance.Key.PublicKey, DataRepository.Instance.Domain);
				Dictionary<string, string> pData = new Dictionary<string, string>
				{
					{"Key", encryptedPublicKey},
					{"Domain", DataRepository.Instance.Domain}
				};

				HttpResponseMessage response1 = await client.PostAsJsonAsync("api/ClientConnection", pData);

				if (! response1.IsSuccessStatusCode)
				{
					return View((object) response1.ToString());
				}

				var response2 = await client.GetAsync("api/Licenses?domain=" + DataRepository.Instance.Domain);

				if (!response2.IsSuccessStatusCode)
				{
					return View((object)response2.ToString());
				}

				var responseContent = await response2.Content.ReadAsAsync<Dictionary<string, string>>();
				byte[] encryptedData = Convert.FromBase64String(responseContent["data"]);

				var licenseData = AsymmetricEncryption.Decrypt(encryptedData, 4096, DataRepository.Instance.Key.PrivateKey);
				LicenseData license = ObjectSerializer.Deserialize(licenseData) as LicenseData;

				//string result = await response.Content.ReadAsStringAsync();
				return View((object)license);
			}

			//return View((object)DataRepository.Instance.RemoteKey);
		}
		/*
				// POST: Home/Create
				[HttpPost]
				public ActionResult Create(FormCollection collection)
				{
					try
					{
						// TODO: Add insert logic here

						return RedirectToAction("Index");
					}
					catch
					{
						return View();
					}
				}

				// GET: Home/Edit/5
				public ActionResult Edit(int id)
				{
					return View();
				}

				// POST: Home/Edit/5
				[HttpPost]
				public ActionResult Edit(int id, FormCollection collection)
				{
					try
					{
						// TODO: Add update logic here

						return RedirectToAction("Index");
					}
					catch
					{
						return View();
					}
				}

				// GET: Home/Delete/5
				public ActionResult Delete(int id)
				{
					return View();
				}

				// POST: Home/Delete/5
				[HttpPost]
				public ActionResult Delete(int id, FormCollection collection)
				{
					try
					{
						// TODO: Add delete logic here

						return RedirectToAction("Index");
					}
					catch
					{
						return View();
					}
				}
		*/

	}
}
