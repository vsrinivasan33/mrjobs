﻿using mrjobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mrjobs.Interface
{
	public interface IAppService
	{
		Task Initialize();

		Task<Client> AddClient(NewClient client);

		Task<List<Client>> GetClientsList();

		Task<List<Job>> GetClientJobs(string clientId);

		Task<Job> SaveJobDetails(Job saveJob);

		Task<bool> DeleteJob(string jobId);
	}
}
