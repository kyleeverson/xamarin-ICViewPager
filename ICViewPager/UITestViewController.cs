using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace ICViewPager
{
	public class UITestViewController : UIViewController
	{
		UILabel textLabel;
		string labelText;

		public UITestViewController (string txt)
		{
			labelText = txt;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.Yellow;
			textLabel = new UILabel (new RectangleF (0, 0, 100, 30));
			textLabel.Text = labelText;
			View.Add (textLabel);
		}
	}
}

