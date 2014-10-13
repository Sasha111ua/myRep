using System;
using MonoTouch.Foundation;
using Cirrious.MvvmCross.Touch.Views;
using OmnicTabs.Core.ViewModels;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.ObjCRuntime;

namespace OmnicTab.Touch
{
	[Register("OmnicTabsView")]
	public sealed class OmnicTabsTouchView : MvxTabBarViewController
	{
		public OmnicTabsTouchView()
		{
			// need this additional call to ViewDidLoad because UIkit creates the view before the C# hierarchy has been constructed
			ViewDidLoad();
		}

		protected OmnicTabsTouchViewModel FirstViewModel
		{ 
			get 
			{ 
				return ViewModel as OmnicTabsTouchViewModel;
			} 
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// ios7 layout
			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			if (ViewModel == null)
				return;

			var viewControllers = new UIViewController[]
			{
				CreateTabFor("1", "home", FirstViewModel.ImageCollection),
				CreateTabFor("2", "locationDb", FirstViewModel.LocationEntities),
				CreateTabFor("3", "map", FirstViewModel.MapOmnic)
			};
			ViewControllers = viewControllers;
			CustomizableViewControllers = new UIViewController[] { };
			SelectedViewController = ViewControllers[0];
		}

		private int _createdSoFarCount = 0;

		private UIViewController CreateTabFor(string title, string imageName, IMvxViewModel viewModel)
		{
			var controller = new UINavigationController();
			var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
			SetTitleAndTabBarItem(screen, title, imageName);
			controller.PushViewController(screen, false);
			return controller;
		}

		private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
		{
			screen.Title = title;
			screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle("Images/Tabs/home.png"),
				_createdSoFarCount);
			_createdSoFarCount++;
		}

		public void ShowGrandChild(IMvxTouchView view)
		{
		    var currentNav = SelectedViewController as UINavigationController;
		    if (currentNav != null) currentNav.PushViewController(view as UIViewController, true);
		}
	}
}

