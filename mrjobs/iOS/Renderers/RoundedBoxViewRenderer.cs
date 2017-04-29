using System;
using mrjobs.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;
using mrjobs.Controls;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]
namespace mrjobs.iOS
{
	public class RoundedBoxViewRenderer : BoxRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);

			if (Element != null)
			{
				Layer.MasksToBounds = true;
				UpdateCornerRadius(Element as RoundedBoxView);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == RoundedBoxView.CornerRadiusProperty.PropertyName)
			{
				UpdateCornerRadius(Element as RoundedBoxView);
			}
		}

		void UpdateCornerRadius(RoundedBoxView box)
		{
			Layer.CornerRadius = (float)box.CornerRadius;
			Layer.BorderWidth = 2;
			Layer.BorderColor = Color.Transparent.ToCGColor();
		}
	}
}
