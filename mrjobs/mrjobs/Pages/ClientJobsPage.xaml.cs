using System;
using System.Collections.Generic;
using mrjobs.Models;
using mrjobs.ViewModels;
using Xamarin.Forms;

namespace mrjobs.Pages
{
	public partial class ClientJobsPage : ContentPage
	{
		public ClientJobsPage(Client client)
		{
			InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, String.Empty);
			BindingContext = new ClientJobsPageViewModel(client);
			listViewJobs.ItemSelected += (sender, e) =>
			{
				if(e.SelectedItem != null)
					Navigation.PushAsync(new JobPage(client, e.SelectedItem as Job));
			};
		}
	}
}
