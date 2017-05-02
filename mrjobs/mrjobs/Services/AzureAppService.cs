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
		IMobileServiceSyncTable _jobSyncTable;

		public async Task Initialize()
		{
			try
			{
				MobileService = new MobileServiceClient(AppConfig.AZURE_MOBILE_SERVICE_URL);
				var localStorage = new MobileServiceSQLiteStore(AppConfig.LOCAL_STORAGE);

				//Define the tables for Storage and Sync
				localStorage.DefineTable<ContactAddress>();
				localStorage.DefineTable<Client>();
				localStorage.DefineTable<Job>();

				await MobileService.SyncContext.InitializeAsync(localStorage);

				_addressSyncTable = MobileService.GetSyncTable<ContactAddress>();
				_clientSyncTable = MobileService.GetSyncTable<Client>();
				_jobSyncTable = MobileService.GetSyncTable<Job>();

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
		}

		public async Task<Job> SaveJobDetails(Job job)
		{
			Job savedJob = null;
			try
			{
				if (string.IsNullOrEmpty(job.Id))
				{
					var clientJobJObj = await _jobSyncTable.InsertAsync(JObject.FromObject(job));
					savedJob = clientJobJObj.ToObject<Job>();
				}
				else
				{
					var jobToSave = _jobSyncTable.LookupAsync(job.Id);
					if (jobToSave != null)
					{
						await _jobSyncTable.UpdateAsync(JObject.FromObject(job));
					}
					savedJob = job;
				}

				await SyncClientJobs();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
			return savedJob;
		}

		public async Task<bool> DeleteJob(string jobId)
		{
			try
			{
				var jobToDelete = await _jobSyncTable.LookupAsync(jobId);
				if (jobToDelete != null)
				{
					await _jobSyncTable.DeleteAsync(JObject.FromObject(jobToDelete));
					await SyncClientJobs();
					return true;
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
			return false;
		}

		public async Task<List<Job>> GetClientJobs(string clientId)
		{
			//Now sync the clients
			await SyncClientJobs();
			var jtoken = await _jobSyncTable.ReadAsync(string.Empty);
			var jobsList = jtoken.ToObject<List<Job>>();
			var clientJobsList = jobsList.Where(x => x.ClientId == clientId);

			foreach (var job in clientJobsList)
			{
				if (!string.IsNullOrWhiteSpace(job.SiteAddressId))
				{
					var address = await GetAddress(job.SiteAddressId);
					if (address != null)
						job.SiteAddress = address;
				}
			}
			return clientJobsList.ToList();
		}

		public async Task<List<Client>> GetClientsList()
		{
			try
			{
				//Need to get the Addresses first
				await SyncAddresses();
				//Now sync the clients
				await SyncClients();
				var jtoken = await _clientSyncTable.ReadAsync(string.Empty);
				var clientList = jtoken.ToObject<List<Client>>();

				foreach (var client in clientList)
				{
					if (!string.IsNullOrWhiteSpace(client.BillingAddressId))
					{
						var address = await GetAddress(client.BillingAddressId);
						if (address != null)
							client.BillingAddress = address;
					}
				}
				return clientList;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
			return new List<Client>();
		}

		public async Task<Client> AddClient(NewClient addClient)
		{
			try
			{
				var newClientAddress = new ContactAddress { AddressLine1 = addClient.AddressLine1, AddressLine2 = addClient.AddressLine2, Suburb = addClient.Suburb, State = addClient.State, PostCode = addClient.PostCode, Country = addClient.Country };
				var addressJobj = await _addressSyncTable.InsertAsync(JObject.FromObject(newClientAddress));
				var billingAddress = addressJobj.ToObject<ContactAddress>();
				await SyncAddresses();

				var newClient = new Client
				{
					Name = addClient.Name,
					ContactNumber = addClient.ContactNumber,
					Email = addClient.Email,
					BillingAddress = billingAddress,
					BillingAddressId = billingAddress.Id
				};
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

		public async Task<ContactAddress> GetAddress(string addressId)
		{
			try
			{
				var addressJobject = await _addressSyncTable.LookupAsync(addressId);
				if (addressJobject != null)
				{
					var matchingAddress = addressJobject.ToObject<ContactAddress>();
					return matchingAddress;
				}
			}
			catch (Exception ex)
			{

			}
			return null;
		}

		public async Task SyncAddresses()
		{
			try
			{
				if (_addressSyncTable != null)
				{
					await _addressSyncTable.PullAsync(null, string.Empty);
					await MobileService.SyncContext.PushAsync();
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
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

		public async Task SyncClientJobs()
		{
			try
			{
				if (_jobSyncTable != null)
				{
					await _jobSyncTable.PullAsync(null, string.Empty);
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
