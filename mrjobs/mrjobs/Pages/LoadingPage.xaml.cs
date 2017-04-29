using Rg.Plugins.Popup.Pages;

namespace mrjobs.Pages
{
	public partial class LoadingPage : PopupPage
	{

		public LoadingPage()
		{
			InitializeComponent();
		}

		public void ShowSuccess()
		{
			activityBusy.IsVisible = false;
			imageSuccess.IsVisible = true;

		}
	}
}
