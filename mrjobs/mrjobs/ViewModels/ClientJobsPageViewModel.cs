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
			}
		}

		public Command LoadClientJobsCommand { get; private set; }

		public ClientJobsPageViewModel(Client client)
		{
			Client = client;

			LoadClientJobsCommand = new Command(async () =>
			{
				var appService = App.Container.Resolve<IAppService>();
				if (appService != null)
				{
					ClientJobs = new ObservableRangeCollection<Job>(await appService.GetClientJobs(_client.Id));
				}
			});

			Task.Run(() => LoadClientJobsCommand.Execute(null));
		}
	}
}
