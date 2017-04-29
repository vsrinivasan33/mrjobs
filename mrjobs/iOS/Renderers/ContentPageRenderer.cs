using mrjobs.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace mrjobs.iOS
{
	public class ContentPageRenderer : PageRenderer
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var contentPage = Element as ContentPage;
			if (contentPage == null)
				return;

			if (NavigationController == null)
				return;

			var navigationItem = NavigationController.TopViewController.NavigationItem;

			UIBarButtonItem[] leftNativeButtons = navigationItem.LeftBarButtonItems;
			UIBarButtonItem[] rightNativeButtons = navigationItem.RightBarButtonItems;
			var leftNativeButtonsList = leftNativeButtons != null ? leftNativeButtons.ToList() : new List<UIBarButtonItem>();
			var rightNativeButtonsList = rightNativeButtons != null ? rightNativeButtons.ToList() : new List<UIBarButtonItem>();

			var newLeftButtons = new List<UIBarButtonItem>();
			var newRightButtons = new List<UIBarButtonItem>();

			rightNativeButtonsList.ForEach(nativeItem =>
			{

				var field = nativeItem.GetType().GetField("_item", BindingFlags.NonPublic | BindingFlags.Instance);
				if (field == null)
					return;

				var info = field.GetValue(nativeItem) as ToolbarItem;
				if (info == null)
					return;

				if (info.Priority == 0)
					newLeftButtons.Add(nativeItem);
				else
					newRightButtons.Add(nativeItem);
			});

			leftNativeButtonsList.ForEach(nativeItem =>
			{
				if (newLeftButtons.All(x => x.Title != nativeItem.Title))
					newLeftButtons.Add(nativeItem);
			});

			navigationItem.RightBarButtonItems = newRightButtons.ToArray();
			navigationItem.LeftBarButtonItems = newLeftButtons.ToArray();
			//Sometimes the CareContacts Home button disappears.This is a work around.
			if (navigationItem.LeftBarButtonItems.Length == 0)
				navigationItem.SetHidesBackButton(false, false);
		}
	}
}
