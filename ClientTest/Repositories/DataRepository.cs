using System;
using System.Collections.Generic;
using ClientTest.Models;

namespace ClientTest.Repositories
{
	public class DataRepository
	{
		#region Singleton

		private static readonly Lazy<DataRepository> instance = new Lazy<DataRepository>(() => new DataRepository());
		public static DataRepository Instance => instance.Value;

		private DataRepository()
		{

		}

		#endregion

		public LocalKey Key { get; set; }

		public string RemoteKey { get; set; }

		public string Domain { get; set; }
	}
}