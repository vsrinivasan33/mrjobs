using System;
using System.Collections.Generic;
using mrjobs.Models;
using mrjobs.Interface;
using System.Threading.Tasks;
using System.Linq;

namespace mrjobs.Services
{
	public class MockAppService : IAppService
	{
		List<Client> _clientList = new List<Client>();
		List<Job> _clientJobsList = new List<Job>();
		public MockAppService()
		{
			var newClient = new Client { Id = "C1", Name = "MYER Groups Pty Ltd.", ContactNumber = "(03)9400 1234", Email = "hello@myer.com.au" };
			newClient.BillingAddress = new Address { Id = "A1", AddressLine1 = "200 Lonsdale Street", Suburb = "Melbourne", State = "VIC", PostCode = "3000" };
			_clientList.Add(newClient);

			newClient = new Client { Id = "C2", Name = "JB HI-FI Australia Pty Ltd.", ContactNumber = "(03)9412 2800", Email = "hello@jbhifi.com.au" };
			newClient.BillingAddress = new Address { Id = "A2", AddressLine1 = "130 Warrigal Road", Suburb = "Chadstone", State = "VIC", PostCode = "3158" };
			_clientList.Add(newClient);

			var newJob1 = new Job { Id = "J1", ClientId = "C1", ContactNumber = "0430200100", ContactName = "Justin Beiber", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(7), Status = JobStatus.Pending };
			var newJob2 = new Job { Id = "J2", ClientId = "C1", ContactNumber = "043000199", ContactName = "Brittney Spears", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-1), Status = JobStatus.Open };
			var newJob3 = new Job { Id = "J3", ClientId = "C1", ContactNumber = "(03)90010100", ContactName = "Enrique Iglesias", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-15), Status = JobStatus.Paid };
			var newJob4 = new Job { Id = "J4", ClientId = "C1", ContactNumber = "04302245134", ContactName = "Stevie Wonder", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-23), Status = JobStatus.Paid };
			var newJob5 = new Job { Id = "J5", ClientId = "C1", ContactNumber = "04334500100", ContactName = "Bruno Mars", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-30), Status = JobStatus.Overdue };
			var newJob6 = new Job { Id = "J6", ClientId = "C2", ContactNumber = "04567020565", ContactName = "Katy Perry", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-7), Status = JobStatus.Paid };
			var newJob7 = new Job { Id = "J7", ClientId = "C2", ContactNumber = "04302045464", ContactName = "Pitbull", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-12), Status = JobStatus.Paid };
			var newJob8 = new Job { Id = "J8", ClientId = "C2", ContactNumber = "04302047878", ContactName = "Ellie Goulding", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-15), Status = JobStatus.Paid };
			var newJob9 = new Job { Id = "J9", ClientId = "C2", ContactNumber = "04302345360", ContactName = "Ariana Grande", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-17), Status = JobStatus.Paid };
			var newJob10 = new Job { Id = "J10", ClientId = "C2", ContactNumber = "0430210896", ContactName = "Zayn", Notes = "Need to get this job done", SameAsBillingAddress = true, StartDate = DateTime.Today.AddDays(-27), Status = JobStatus.Paid };

			_clientJobsList.Add(newJob1);
			_clientJobsList.Add(newJob2);
			_clientJobsList.Add(newJob3);
			_clientJobsList.Add(newJob4);
			_clientJobsList.Add(newJob5);
			_clientJobsList.Add(newJob6);
			_clientJobsList.Add(newJob7);
			_clientJobsList.Add(newJob8);
			_clientJobsList.Add(newJob9);
			_clientJobsList.Add(newJob10);
		}

		public async Task<bool> DeleteJob(string jobId)
		{
			return await Task.Run(() =>
			{
				var delJob = _clientJobsList.FirstOrDefault(j => j.Id == jobId);
				if (delJob != null)
				{
					_clientJobsList.Remove(delJob);
					return true;
				}
				return false;
			});
		}

		public async Task<List<Job>> GetClientJobs(string clientId)
		{
			return await Task.FromResult(_clientJobsList.Where(x => x.ClientId == clientId).ToList());
		}

		public async Task<List<Client>> GetClientsList()
		{
			return await Task.FromResult(_clientList);
		}

		public Task<Job> SaveJobDetails(Job saveJob)
		{
			throw new NotImplementedException();
		}
	}
}
