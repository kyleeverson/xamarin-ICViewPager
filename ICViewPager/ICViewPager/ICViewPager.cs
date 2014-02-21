using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.ObjCRuntime;

namespace ICViewPager
{
	public class ViewPagerItem : NSObject
	{
		public UIViewController content { get; set; }
		public string tabName { get; set; }

		public ViewPagerItem()
		{
		}
	}

	public class ViewPagerSource : NSObject
	{
		public List<ViewPagerItem> items { get; set; }

		public float tabHeight { get; set; }
		public float tabWidth { get; set; }

		public ViewPagerSource()
		{
			items = new List<ViewPagerItem> ();
			tabHeight = 44.0f;
			tabWidth = 128.0f;
		}

		public void AddItem(ViewPagerItem item)
		{
			items.Add (item);
		}

		public void ResetList()
		{
			items.RemoveRange (0, items.Count);
		}
	}

	public class ViewPagerController : UIViewController
	{
		ViewPagerSource pagerSource;

		UIScrollView tabsView;

		List<TabElement> tabElements;

		public ViewPagerController (ViewPagerSource source)
		{
			pagerSource = source;
			tabElements = new List<TabElement> ();
		}

		[Export ("MySelector")]
		public void MyObjectiveCHandler (UITapGestureRecognizer rec)
		{
			PointF pt = rec.LocationInView (tabsView);
			int index = (int)(pt.X / pagerSource.tabWidth);
			SelectTabAtIndex (index);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UIGestureRecognizer gesture = new UITapGestureRecognizer ();
			gesture.AddTarget (this, new Selector ("MySelector"));

			RectangleF rect = View.Frame;

			tabsView = new UIScrollView(new RectangleF(0, rect.Bottom - pagerSource.tabHeight, rect.Width, pagerSource.tabHeight));
			tabsView.AddGestureRecognizer (gesture);
			tabsView.BackgroundColor = UIColor.LightGray;
			tabsView.ShowsHorizontalScrollIndicator = false;
			tabsView.ShowsVerticalScrollIndicator = false;
			tabsView.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
			View.Add (tabsView);

			reloadData ();

			SelectTabAtIndex (0);
		}

		/**
		 * Reloads all tabs and contents
		 */
		public void reloadData()
		{
			for (int i = 0; i < tabElements.Count; i++) {
				tabElements [i].RemoveFromSuperview ();
			}

			tabElements.RemoveRange (0, tabElements.Count);
			for (int i = 0; i < pagerSource.items.Count; i++) {
				TabElement element = new TabElement (new RectangleF (i * pagerSource.tabWidth, 0, pagerSource.tabWidth, pagerSource.tabHeight));
				element.tabLabel.Text = pagerSource.items [i].tabName;
				element.Enabled = false;
				tabsView.Add (element);
				tabElements.Add (element);
			}
			tabsView.ContentSize = new SizeF (pagerSource.items.Count * pagerSource.tabWidth, pagerSource.tabHeight);
		}

		/**
		 * Selects the given tab and shows the content at this index
		 *
		 * @param index The index of the tab that will be selected
		 */
		public void SelectTabAtIndex(int index)
		{
			for (int i = 0; i < tabElements.Count; i++) {
				tabElements [i].SetActive (i == index);
				pagerSource.items [i].content.View.RemoveFromSuperview ();
				if (i == index) {
					View.Add (pagerSource.items [i].content.View);
					pagerSource.items [i].content.View.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Height - pagerSource.tabHeight);
				}
			}
			RectangleF rect = new RectangleF (index * pagerSource.tabWidth, 0, pagerSource.tabWidth, pagerSource.tabHeight);
			tabsView.ScrollRectToVisible (rect, true);
		}

		class TabElement : UIControl
		{
			public UILabel tabLabel { get; set; }
			public UIView highlightBlock { get; set; }

			public TabElement(RectangleF frame)
			{
				Frame = frame;

				tabLabel = new UILabel();
				tabLabel.Frame = new RectangleF(0, 0, frame.Width, 30);
				tabLabel.TextAlignment = UITextAlignment.Center;

				highlightBlock = new UIView();
				highlightBlock.BackgroundColor = UIColor.LightGray;
				highlightBlock.Frame = new RectangleF(0, frame.Height-5, frame.Width, 5);

				Add(tabLabel);
				Add(highlightBlock);

				BackgroundColor = UIColor.LightGray;
			}

			public void SetActive(bool flag)
			{
				if (flag == true) {
					highlightBlock.BackgroundColor = UIColor.Red;
				} else {
					highlightBlock.BackgroundColor = UIColor.LightGray;
				}
			}
		}
	}
}

