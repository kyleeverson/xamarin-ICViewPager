using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace ICViewPager
{
	public class UITestViewController : UIViewController
	{
		UILabel textLabel;

		public UITestViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.Yellow;
			textLabel = new UILabel (new RectangleF (0, 0, 100, 30));
			textLabel.Text = "ABC";
			View.Add (textLabel);
		}
	}
}

