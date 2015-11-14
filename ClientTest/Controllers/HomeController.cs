using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using ClientTest.Models;
using ClientTest.Repositories;
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
		public async System.Threading.Tasks.Task<ActionResult> Create(DomainData data)
		{
			SymmetricEncryption se = new SymmetricEncryption();

			DataRepository.Instance.Domain = data.Domain;
			DataRepository.Instance.RemoteKey = se.Decrypt(data.Key, data.Domain);

/*
			string privateKey;
			string publicKey;
			AsymmetricEncryption.GenerateKeys(1024, out privateKey, out publicKey);

			DataRepository.Instance.Key = new LocalKey
			{
				PrivateKey = privateKey,
				PublicKey = publicKey
			};

			// TODO: Public key will be sent to server

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:4427/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				Tuple<string, string> pData = new Tuple<string, string>(DataRepository.Instance.Key.PublicKey,
					DataRepository.Instance.Domain);

				HttpResponseMessage response = await client.PostAsJsonAsync("api/ClientConnection", pData);

				if (! response.IsSuccessStatusCode)
				{
					string statusCode = response.StatusCode.ToString();
					return View((object) statusCode);
				}
			}
*/

			return View((object)DataRepository.Instance.RemoteKey);
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
