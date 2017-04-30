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
				if (e.SelectedItem != null)
					Navigation.PushAsync(new JobPage(client, e.SelectedItem as Job));
				//Remove the highlighted selection once clicked
				((ListView)sender).SelectedItem = null;
			};
		}

		private void OnBindingContextChanged(object sender, EventArgs e)
		{
			base.OnBindingContextChanged();

			if (BindingContext != null)
			{
				ViewCell viewCell = ((ViewCell)sender);
				var currenJob = viewCell.BindingContext as Job;
				if(currenJob != null && currenJob.Status != JobStatus.Open)
					viewCell.ContextActions.Clear();
			}
		}

		private void DeleteJob_Clicked(object sender, EventArgs e)
		{
			var menuItem = ((MenuItem)sender);
			if (menuItem != null && menuItem.CommandParameter != null)
			{

			}
		}
	}
}
