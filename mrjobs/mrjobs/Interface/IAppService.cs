using MrJobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrJobs.Interface
{
	public interface IAppService
	{
		Task<List<Client>> GetClientsList();

		Task<List<Job>> GetClientJobs(long clientId);

		Task<Job> SaveJobDetails(Job saveJob);

		Task<bool> DeleteJob(long jobId);
	}
}
