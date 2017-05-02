using System;
using System.Threading.Tasks;
using mrjobs.Models;
using MvvmHelpers;
using Xamarin.Forms;
using Microsoft.Practices.Unity;
using mrjobs.Interface;

namespace mrjobs.ViewModels
{
	public class ClientJobsPageViewModel : BaseViewModel
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

		ObservableRangeCollection<Job> _clientJobs;
		public ObservableRangeCollection<Job> ClientJobs
		{
			get { return _clientJobs; }
			set
			{
				_clientJobs = value;
				OnPropertyChanged(nameof(ClientJobs));
				JobCount = value != null ? value.Count : 0;
			}
		}

		int _jobCount;
		public int JobCount
		{
 			get { return _jobCount; }
			set 
			{
				_jobCount = value;
				OnPropertyChanged(nameof(JobCount));
			}
		}

		public Command LoadClientJobsCommand { get; private set; }

		public Command<string> DeleteJobCommand { get; private set; }

		public ClientJobsPageViewModel(Client client)
		{
			Client = client;

			DeleteJobCommand = new Command<string>(async (jobId) =>
			{
			var appService = App.Container.Resolve<IAppService>();
				if (appService != null)
				{
					var deleted = await appService.DeleteJob(jobId);
					//Fire a refresh of the listView
					if (deleted)
					{
						await Task.Run(() => LoadClientJobsCommand.Execute(null));
					}
				}
			});

			LoadClientJobsCommand = new Command(async () =>
			{
				if (IsBusy)
					return;
				
				try
				{
					IsBusy = true;
					var appService = App.Container.Resolve<IAppService>();
					if (appService != null)
					{
						ClientJobs = new ObservableRangeCollection<Job>(await appService.GetClientJobs(_client.Id));
					}
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex);
				}
				finally
				{
					IsBusy = false;
				}
			});

			//Task.Run(() => LoadClientJobsCommand.Execute(null));
		}
	}
}
