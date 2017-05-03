using mrjobs.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(FlatButtonRenderer))]
namespace mrjobs.Droid
{
	public class FlatButtonRenderer : ButtonRenderer
	{
		protected override void OnDraw(Android.Graphics.Canvas canvas)
		{
			base.OnDraw(canvas);
		}
	}
}