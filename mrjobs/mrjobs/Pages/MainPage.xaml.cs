using mrjobs.ViewModels;
using Xamarin.Forms;

namespace mrjobs.Pages
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, true);
			NavigationPage.SetBackButtonTitle(this, string.Empty);
			BindingContext = new MainPageViewModel();
			buttonClients.Clicked += (sender, e) => Navigation.PushAsync(new ClientsPage());
		}
	}
}
