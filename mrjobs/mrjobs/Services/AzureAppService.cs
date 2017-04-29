using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mrjobs.Interface;
using mrjobs.Models;

namespace mrjobs.Services
{
	public class AzureAppService : IAppService
	{
		public AzureAppService()
		{
		}

		public Task<bool> DeleteJob(string jobId)
		{
			throw new NotImplementedException();
		}

		public Task<List<Job>> GetClientJobs(string clientId)
		{
			throw new NotImplementedException();
		}

		public Task<List<Client>> GetClientsList()
		{
			return Task.FromResult(new List<Client>());
		}

		public Task<Job> SaveJobDetails(Job saveJob)
		{
			throw new NotImplementedException();
		}
	}
}
