using Microsoft.Practices.Unity;
using mrjobs.Interface;
using mrjobs.Services;
using mrjobs.Pages;
using Xamarin.Forms;

namespace mrjobs
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

		public static UnityContainer Container { get; set; }

		protected override void OnStart()
		{
			// Handle when your app starts
			Container = new UnityContainer();

			Container.RegisterType<IAppService, MockAppService>();
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
