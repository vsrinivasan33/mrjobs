using System;
using mrjobs.Models;
using mrjobs.Interface;
using MvvmHelpers;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;

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


		public string SaveButtonText
		{
			get { return Job != null && Job.Status != JobStatus.Paid ? "SAVE" : String.Empty; }
		}

		public JobsPageViewModel(Client client, Job job)
		{
			Client = client;
			Job = job;
		}

		internal async Task<bool> SaveJobDetails()
		{
			var appService = App.Container.Resolve<IAppService>();
			if (appService != null)
			{
				var job = await appService.SaveJobDetails(Job);
				return true;
			}
			return false;
		}
	}
}
