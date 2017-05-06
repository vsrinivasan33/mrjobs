using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using mrjobs.Interface;
using mrjobs.Models;
using MvvmHelpers;

namespace mrjobs.ViewModels
{
	public class AddClientPageViewModel : BaseViewModel
	{
		NewClient _client = new NewClient();
		public NewClient Client
		{
			get { return _client; }
			set
			{
				_client = value;
				OnPropertyChanged(nameof(Client));
			}
		}

		public async Task<bool> AddClient()
		{
			var appService = App.Container.Resolve<IAppService>();
			if (appService != null)
			{
				
				var newClient = await appService.AddClient(Client);
				if (newClient != null)
					return true;
			}
			return false;
		}
	}
}
