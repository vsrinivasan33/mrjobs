using System;
using System.Collections.Generic;
using mrjobs.Models;
using Xamarin.Forms;
using mrjobs.ViewModels;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace mrjobs.Pages
{
	public partial class JobPage : ContentPage
	{
		public JobPage(Client client, Job clientJob)
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, true);
			NavigationPage.SetHasBackButton(this, false);
			BindingContext = new JobsPageViewModel(client, clientJob);
			buttonStartDate.Clicked += (sender, e) => datePickerStartDate.Focus();
		}

		void ButtonClose_Clicked(object sender, EventArgs e)
		{
			Navigation.PopAsync(true);
		}

		async void ButtonSave_Clicked(object sender, EventArgs e)
		{
			//If Paid Invoice, then no need to action
			if (string.IsNullOrWhiteSpace(buttonSave.Text))
				return;

			//Show the loading page
			var loadingPage = new LoadingPage();
			await PopupNavigation.PushAsync(loadingPage);
			var vm = BindingContext as JobsPageViewModel;
			if (vm != null)
			{
				var success = await vm.SaveJobDetails();
				if (success)
				{
					loadingPage.ShowSuccess();
					await Task.Delay(2000);
					await Task.WhenAll(
						PopupNavigation.PopAsync(),
						Navigation.PopAsync(true)
					);
					
				}
			}

		}
	}
}
