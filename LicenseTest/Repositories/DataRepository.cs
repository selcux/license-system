using System;
using System.Collections.Generic;
using LicenseTest.Models;

namespace LicenseTest.Repositories
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

		public List<RegisterData> RegisterList { get; } = new List<RegisterData>();

		public List<LocalKey> LocalKeys { get; } = new List<LocalKey>();

		public List<ClientData> ClienDataList { get; } = new List<ClientData>(); 
	}
}