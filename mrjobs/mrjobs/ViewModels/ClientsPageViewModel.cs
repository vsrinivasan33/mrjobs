using Xamarin.Forms;
using Microsoft.Practices.Unity;
using mrjobs.Interface;
using System.Threading.Tasks;
using MvvmHelpers;
using mrjobs.Models;

namespace mrjobs.ViewModels
{
	public class ClientsPageViewModel : BaseViewModel
	{
		public Command LoadClientsCommand { get; private set; }

		ObservableRangeCollection<Client> clients;
		public ObservableRangeCollection<Client> Clients
		{
			get { return clients; }
			set
			{
				clients = value;
				OnPropertyChanged(nameof(Clients));
			}
		}

		public ClientsPageViewModel()
		{
			LoadClientsCommand = new Command(async () =>
			{
				if (IsBusy)
					return;
				try
				{
					IsBusy = true;
					var appService = App.Container.Resolve<IAppService>();
					if (appService != null)
					{
						await appService.Initialize();
						var clientList = await appService.GetClientsList();
						Clients = new ObservableRangeCollection<Client>(clientList);
					}
				}
				catch (System.Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex);
				}
				finally
				{
					IsBusy = false;
				}
			});
			//Fire the first call on initial load
			Task.Run(() => LoadClientsCommand.Execute(null));
		}
	}
}
