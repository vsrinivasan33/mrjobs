using System;
using System.Collections.Generic;
using mrjobs.Models;
using mrjobs.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mrjobs.Pages
{
	public partial class ClientsPage : ContentPage
	{
		public ClientsPage()
		{
			InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, String.Empty);
			BindingContext = new ClientsPageViewModel();
			listViewClients.ItemSelected += (sender, e) => Navigation.PushAsync(new ClientJobsPage(e.SelectedItem as Client));
		}

		void ButtonAdd_Clicked(object sender, System.EventArgs e)
		{
			PopupNavigation.PushAsync(new AddClientPage());
		}
	}
}
