using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using mrjobs.Config;
using mrjobs.Interface;
using mrjobs.Models;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace mrjobs.Services
{
	public class AzureAppService : IAppService
	{
		public MobileServiceClient MobileService { get; set; }

		IMobileServiceSyncTable _addressSyncTable;
		IMobileServiceSyncTable _clientSyncTable;
		
		public async Task Initialize()
		{
			try
			{
				MobileService = new MobileServiceClient(AppConfig.AZURE_MOBILE_SERVICE_URL);
				var localStorage = new MobileServiceSQLiteStore(AppConfig.LOCAL_STORAGE);

				//Define the tables for Storage and Sync
				localStorage.DefineTable<Address>();
				localStorage.DefineTable<Client>();

				await MobileService.SyncContext.InitializeAsync(localStorage);

				_addressSyncTable = MobileService.GetSyncTable<Address>();
				_clientSyncTable = MobileService.GetSyncTable<Client>();

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
		}

		public async Task<bool> DeleteJob(string jobId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Job>> GetClientJobs(string clientId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Client>> GetClientsList()
		{
			//return await Task.FromResult(new List<Client>());
			await SyncClients();
			var jtoken = await _clientSyncTable.ReadAsync(string.Empty);
			var clientList = jtoken.ToObject<List<Client>>();
			return clientList;
		}

		public async Task<Client> AddClient(NewClient addClient)
		{
			try
			{
				var newClientAddress = new Address { Id="A1" ,AddressLine1 = addClient.AddressLine1, AddressLine2 = addClient.AddressLine2, Suburb = addClient.Suburb, State = addClient.State, PostCode = addClient.PostCode, Country = addClient.Country };

				var adddObj = await _addressSyncTable.InsertAsync(JObject.FromObject(newClientAddress));
				await SyncAddresses();

				var newClient = new Client { Name = addClient.Name, ContactNumber = addClient.ContactNumber, Email = addClient.Email, BillingAddress = newClientAddress };
				var clientJObj = await _clientSyncTable.InsertAsync(JObject.FromObject(newClient));
				await SyncClients();

				return clientJObj.ToObject<Client>();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
			return null;

		}

		public async Task<Job> SaveJobDetails(Job saveJob)
		{
			return await Task.FromResult<Job>(null);
		}

		public async Task SyncAddresses()
		{
			try
			{
				if (_addressSyncTable != null)
				{
					await _addressSyncTable.PullAsync(null, string.Empty);
					//await MobileService.SyncContext.PushAsync();
				}
			}
			catch (Exception ex)
			{
				
			}
			finally
			{

			}
		}

		public async Task SyncClients()
		{
			try
			{
				if (_clientSyncTable != null)
				{
					await _clientSyncTable.PullAsync(null, string.Empty);
					await MobileService.SyncContext.PushAsync();
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
		}
	}
}
