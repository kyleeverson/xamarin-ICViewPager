using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;

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

		public ViewPagerSource()
		{
			items = new List<ViewPagerItem> ();
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
		public float ViewPagerOptionTabHeight { get; set; }
		public float ViewPagerOptionTabOffset { get; set; }
		public float ViewPagerOptionTabWidth { get; set; }
		public float ViewPagerOptionTabLocation { get; set; }
		public float ViewPagerOptionStartFromSecondTab { get; set; }
		public float ViewPagerOptionCenterCurrentTab { get; set; }
		public float ViewPagerOptionFixFormerTabsPositions { get; set; }
		public float ViewPagerOptionFixLatterTabsPositions { get; set; }

		ViewPagerSource pagerSource;

		UIScrollView tabsView;
		UIView contentView;

		List<TabElement> tabElements;

		public ViewPagerController (ViewPagerSource source)
		{
			ViewPagerOptionTabHeight = 44.0f;
			ViewPagerOptionTabOffset = 56.0f;
			ViewPagerOptionTabWidth = 128.0f;
			ViewPagerOptionTabLocation = 1.0f;
			ViewPagerOptionStartFromSecondTab = 0.0f;
			ViewPagerOptionCenterCurrentTab = 0.0f;
			ViewPagerOptionFixFormerTabsPositions = 0.0f;
			ViewPagerOptionFixLatterTabsPositions = 0.0f;

			pagerSource = source;
			tabElements = new List<TabElement> ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RectangleF rect = View.Frame;

			tabsView = new UIScrollView(new RectangleF(0, rect.Bottom - ViewPagerOptionTabHeight, rect.Width, ViewPagerOptionTabHeight));
			tabsView.BackgroundColor = UIColor.Blue;
			View.Add (tabsView);

			reloadData ();
		}


		/*		*
		 * Reloads all tabs and contents
		 */
		public void reloadData()
		{
			tabElements.RemoveRange (0, tabElements.Count);
			for (int i = 0; i < pagerSource.items.Count; i++) {
				TabElement element = new TabElement (new RectangleF (i * ViewPagerOptionTabWidth, 0, ViewPagerOptionTabWidth, ViewPagerOptionTabHeight));
				element.tabLabel.Text = pagerSource.items [i].tabName;
				tabsView.Add (element);
			}
			tabsView.ContentSize = new SizeF (pagerSource.items.Count * ViewPagerOptionTabWidth, ViewPagerOptionTabHeight);
		}

		/*		*
		 * Selects the given tab and shows the content at this index
		 *
		 * @param index The index of the tab that will be selected
		 */
		public void selectTabAtIndex(int index)
		{
		}

		/*		*
		 * Reloads the appearance of the tabs view. 
		 * Adjusts tabs' width, offset, the center, fix former/latter tabs cases.
		 * Without implementing the - viewPager:valueForOption:withDefault: delegate method, 
		 * this method does nothing.
		 * Calling this method without changing any option will affect the performance.
		 */
		public void setNeedsReloadOptions()
		{
		}

		/*		*
		 * Reloads the colors.
		 * You can make ViewPager to reload its components colors.
		 * Changing `ViewPagerTabsView` and `ViewPagerContent` color will have no effect to performance,
		 * but `ViewPagerIndicator`, as it will need to iterate through all tabs to update it.
		 * Calling this method without changing any color won't affect the performance, 
		 * but will cause your delegate method (if you implemented it) to be called three times.
		 */
		public void setNeedsReloadColors()
		{
		}

		/*		*
		 * Call this method to get the value of a given option.
		 * Returns NAN for any undefined option.
		 *
		 * @param option The option key. Keys are defined in ViewPagerController.h
		 *
		 * @return A CGFloat, defining the setting for the given option
		 */
//		public float valueForOption(ViewPagerOption option)
//		{
//
//			return 0.0f;
//		}

		/*		*
		 * Call this method to get the color of a given component.
		 * Returns [UIColor clearColor] for any undefined component.
		 *
		 * @param component The component key. Keys are defined in ViewPagerController.h
		 *
		 * @return A UIColor for the given component
		 */
//		public UIColor colorForComponent(ViewPagerComponent component)
//		{
//
//			return null;
//		}

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
				highlightBlock.BackgroundColor = UIColor.Yellow;
				highlightBlock.Frame = new RectangleF(0, frame.Height-5, frame.Width, 5);

				Add(tabLabel);
				Add(highlightBlock);

				BackgroundColor = UIColor.Brown;
			}
		}
	}
}

