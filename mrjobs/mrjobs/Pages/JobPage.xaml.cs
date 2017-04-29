using System;
using System.Collections.Generic;
using mrjobs.Models;
using Xamarin.Forms;
using mrjobs.ViewModels;

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
		}
	}
}
