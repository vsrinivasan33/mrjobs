using System;
using mrjobs.Models;
using mrjobs.Interface;
using MvvmHelpers;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using mrjobs.Pages;
using mrjobs.Config;

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
				if (value != null)
				{
					NewJob = value != null && String.IsNullOrWhiteSpace(value.Id);
					JobActionText = String.IsNullOrWhiteSpace(value.Id) ? AppConfig.CREATE_NEW_JOB
										  : (value.Status == JobStatus.Paid ? AppConfig.SAVE_JOB_DETAILS : AppConfig.CLOSE_THE_JOB);

					string textToDisplay = string.Empty;
					switch ((int)value.Status)
					{
						case 0: //Open
							if (Job.StartDate >= DateTime.Today)
								textToDisplay = AppConfig.CREATE_NEW_JOB;
							else
								textToDisplay = AppConfig.CREATE_INVOICE;
							break;
						case 1: //Pending Payment
							if (Job.InvoicedDate < DateTime.Today.AddMonths(-1))
								textToDisplay = AppConfig.MARK_OVERDUE;
							else
								textToDisplay = AppConfig.CLOSE_THE_JOB;
							break;
						case 2:// overdue
							textToDisplay = AppConfig.CLOSE_THE_JOB;
							break;
						case 3: //paid
							textToDisplay = AppConfig.SAVE_JOB_DETAILS;
							break;
					}
					JobActionText = textToDisplay;
				}
				OnPropertyChanged(nameof(Job));
				OnPropertyChanged(nameof(JobTitle));
			}
		}

		bool _newJob;

		public bool NewJob
		{
			get { return _newJob; }
			set { _newJob = value; OnPropertyChanged(nameof(NewJob)); }

		}

		string _jobActionText;

		public string JobActionText
		{
			get
			{
				return _jobActionText;
			}

			set
			{
				_jobActionText = value;
				OnPropertyChanged(nameof(JobActionText));
			}
		}

		public string JobTitle
		{
			get { return Job != null && !string.IsNullOrEmpty(Job.Id) ? Job.JobTitle : "NEW JOB"; }
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

		internal void SubscribeToChanges()
		{
			MessagingCenter.Subscribe<JobNotesPage, string>(this, "JobNotes", (sender, notes) =>
			{
				//Unsubscribe to the events
				MessagingCenter.Unsubscribe<JobNotesPage, string>(this, "JobNotes");
				Job.Notes = notes;
				OnPropertyChanged(nameof(Job));
			});
		}

		internal async Task<bool> ExecuteJobAction()
		{
			var appService = App.Container.Resolve<IAppService>();
			if (appService != null)
			{
				switch (JobActionText)
				{
					case AppConfig.CREATE_INVOICE:
						_job.Status = JobStatus.Pending;
						_job.InvoicedDate = DateTime.Now;
						break;
					case AppConfig.MARK_OVERDUE:
						_job.Status = JobStatus.Overdue;
						break;
					case AppConfig.CLOSE_THE_JOB:
						{
							_job.Status = JobStatus.Paid;
						}
						break;
				}
				bool success = await SaveJobDetails();
				//Prompt the UI to show the status only if successfully saved
				if (success)
					OnPropertyChanged(nameof(Job));
				return success;
			}
			return false;
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
