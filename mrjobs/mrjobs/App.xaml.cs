using MrJobs.Pages;
using Xamarin.Forms;

namespace MrJobs
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			NavigationPage navPage = new NavigationPage(new MainPage());
			navPage.BarBackgroundColor = Color.FromHex("5392DC");
			navPage.BarTextColor = Color.White;
			MainPage = navPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
