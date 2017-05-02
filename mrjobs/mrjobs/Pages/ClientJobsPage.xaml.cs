using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mrjobs.Models;
using mrjobs.ViewModels;
using Xamarin.Forms;

namespace mrjobs.Pages
{
	public partial class ClientJobsPage : ContentPage
	{
		private Client _client;

		public ClientJobsPage(Client client)
		{
			InitializeComponent();
			_client = client;
			NavigationPage.SetBackButtonTitle(this, String.Empty);
			BindingContext = new ClientJobsPageViewModel(_client);
			listViewJobs.ItemSelected += (sender, e) =>
			{
				if (e.SelectedItem != null)
					Navigation.PushAsync(new JobPage(_client, e.SelectedItem as Job));
				//Remove the highlighted selection once clicked
				((ListView)sender).SelectedItem = null;
			};
		}

		void ButtonAdd_Clicked(object sender, System.EventArgs e)
		{
			Job newJob = new Job();
			newJob.ClientId = _client.Id;
			newJob.StartDate = DateTime.Today;
			newJob.SameAsBillingAddress = true;
			newJob.SiteAddressId = _client.BillingAddressId;
			newJob.SiteAddress = _client.BillingAddress;
			      
			Navigation.PushAsync(new JobPage(_client, newJob));
		}

		private void OnBindingContextChanged(object sender, EventArgs e)
		{
			base.OnBindingContextChanged();

			if (BindingContext != null)
			{
				ViewCell viewCell = ((ViewCell)sender);
				var currenJob = viewCell.BindingContext as Job;
				if (currenJob != null && currenJob.Status != JobStatus.Open)
					viewCell.ContextActions.Clear();
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			var vm = BindingContext as ClientJobsPageViewModel;
			if (vm != null)
			{
				Task.Run(() => vm.LoadClientJobsCommand.Execute(null));
			}
		}
	}
}
