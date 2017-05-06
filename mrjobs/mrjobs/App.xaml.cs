using Microsoft.Practices.Unity;
using mrjobs.Interface;
using mrjobs.Services;
using mrjobs.Pages;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace mrjobs
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			NavigationPage navPage = new NavigationPage(new MainPage());
			navPage.BarBackgroundColor = (Color)Resources["AppNavBarColor"];
			navPage.BarTextColor = Color.White;
			MainPage = navPage;
		}

		public static UnityContainer Container { get; set; }

		protected override void OnStart()
		{
			// Handle when your app starts
			Container = new UnityContainer();
#if RELEASE
			Container.RegisterType<IAppService, MockAppService>(new ContainerControlledLifetimeManager());
#else
			Container.RegisterType<IAppService, AzureAppService>(new ContainerControlledLifetimeManager());
#endif

			//Initialize the MobileService here...
			Task.Run(async () =>
			{
				var appService = Container.Resolve<IAppService>();
				if (appService != null)
				{
					await appService.Initialize();
				}
			});
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
