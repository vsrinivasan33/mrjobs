using MrJobs.ViewModels;
using Xamarin.Forms;

namespace MrJobs.Pages
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, true);
			BindingContext = new MainPageViewModel();
		}
	}
}
