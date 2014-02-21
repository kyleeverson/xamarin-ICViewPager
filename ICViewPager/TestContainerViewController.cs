using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace ICViewPager
{
	public class TestContainerViewController : UIViewController
	{
		ViewPagerController myViewController;
		UIBarButtonItem testButton;

		public TestContainerViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (UIDevice.CurrentDevice.SystemVersion.CompareTo("7") > 0) {
				this.EdgesForExtendedLayout = UIRectEdge.None;
			}

			testButton = new UIBarButtonItem ("Tab #5", UIBarButtonItemStyle.Plain, null);
			testButton.Clicked += (sender, e) => { 
				myViewController.SelectTabAtIndex(5); 
			};

			RectangleF rect = View.Frame;

			ViewPagerSource pagerSource = new ViewPagerSource ();
			pagerSource.tabHeight = 44;
			pagerSource.tabWidth = 120;

			for (int i = 0; i < 15; i++) {
				UITestViewController testController = new UITestViewController (string.Format("View {0}", i));

				ViewPagerItem item = new ViewPagerItem ();
				item.content = testController;
				item.tabName = string.Format("Tab #{0}", i);

				pagerSource.AddItem (item);
			}

			myViewController = new ViewPagerController (pagerSource);
			myViewController.View.Frame = new RectangleF (0, 0, rect.Width, rect.Height);
			View.Add (myViewController.View);
			AddChildViewController (myViewController);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			NavigationItem.Title = "Test App";
			NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { testButton }, false);
		}
	}
}

