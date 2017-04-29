using System;
using mrjobs.Models;
using MvvmHelpers;

namespace mrjobs.ViewModels
{
	public class JobsPageViewModel : BaseViewModel
	{
		Client _client;
		public Client Client
		{
			get { return _client; }
			set 
			{
				_client = value;
				OnPropertyChanged(nameof(Client));
			}
		}

		Job _job;
		public Job Job
		{
			get { return _job; }
			set
			{
				_job = value;
				OnPropertyChanged(nameof(Job));
				OnPropertyChanged(nameof(JobTitle));
			}
		}

		public string JobTitle
		{
			get { return Job != null && !string.IsNullOrEmpty(Job.Id) ? string.Format("JOB {0}", Job.Id) : "NEW JOB"; }
		}

		public JobsPageViewModel(Client client, Job job)
		{
			Client = client;
			Job = job;
		}
	}
}
