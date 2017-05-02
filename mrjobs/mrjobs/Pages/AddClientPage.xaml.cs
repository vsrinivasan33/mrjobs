using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mrjobs.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mrjobs.Pages
{
	public partial class AddClientPage : PopupPage
	{
		public AddClientPage()
		{
			InitializeComponent();
			BindingContext = new AddClientPageViewModel();
			buttonAddClient.Clicked += async (sender, e) =>
			{
				var loadingPage = new LoadingPage();
				await PopupNavigation.PushAsync(loadingPage);
				var vm = BindingContext as AddClientPageViewModel;
				if (vm != null)
				{
					var success = await vm.AddClient();
					if (success)
					{
						loadingPage.ShowSuccess();
						await Task.Delay(2000);
						//Pop both the success and the add new client pages
						await PopupNavigation.PopAllAsync();
					}
				}
			};
		}

		void ButtonClose_Clicked(object sender, EventArgs e)
		{
			PopupNavigation.PopAsync();
		}
	}
}
