## C# Version of ViewPagerController

This is a ViewController that shows horizontal scrollable tabs along the bottom of the view which when selected, bring up a new ViewController in the section of the Pager Controller.

### Screen Shot
<img src="https://raw2.github.com/kyleeverson/xamarin-ICViewPager/master/ViewPagerController.png" width=240 alt="ICViewPager" title="ICViewPager">

To use this control in your projects, take the cs file in the ICViewPager folder and place in your application.

Here is a sample of creating an ViewPagerController.  First create the ViewPagerSource, this will contain the UIViewController and the tab name for each Page.  This sample adds 15 UIViewControllers to the ViewPagerController and sets the tabHeight to 44 and the width for each tab to 120.

```C#
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
```

### XCode Version
This project was inspired from the XCode version located at [/monsieurje/ICViewPager](https://github.com/monsieurje/ICViewPager).
