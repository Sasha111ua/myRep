using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Cirrious.MvvmCross.Binding.Touch.Views;
using OmnicTabs.Core.ViewModels;
using OmnicTabs.Core.Services;
using OmnicTabs.Core.BusinessLayer;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Location;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;


namespace Tabbed.Touch.Views
{
	/*[Register("OmnicTabsView")]
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
				//if (ViewModel != null)
				return ViewModel as OmnicTabsTouchViewModel;
				//return new OmnicTabsViewModel(Mvx.Resolve<IMvxLocationWatcher>());
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
				CreateTabFor("2", "twitter", FirstViewModel.LocationEntities),
				CreateTabFor("3", "favorites", FirstViewModel.MapOmnic),
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
			currentNav.PushViewController(view as UIViewController, true);
		}
	}


	public class ImageCollectionView : MvxViewController
	{
		public override void ViewDidLoad()
		{
			View = new UIView() { BackgroundColor = UIColor.Clear};
			base.ViewDidLoad();
			var viewModel = (ImageCollectionViewModel)ViewModel;
			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;
			//TableView = new UITableView ();
			var refreshButton = UIButton.FromType(UIButtonType.RoundedRect);
			refreshButton.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			refreshButton.Frame = new RectangleF (0,0, 0, 20);
			refreshButton.BackgroundColor = UIColor.Blue;
			this.CreateBinding (refreshButton).To<ImageCollectionViewModel> (vm => vm.RefreshCommand).Apply ();
			refreshButton.SetTitle ("Refresh", UIControlState.Normal);
			View.AddSubview (refreshButton);
			var table = new UITableView(new RectangleF(0, refreshButton.Frame.Height, 0, 450));
			table.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			var source = new MvxStandardTableViewSource(table, "ImageUrl Url");
			table.Source = source;
			this.CreateBinding(source).To<ImageCollectionViewModel>(vm => vm.Images).Apply();
			//this.CreateBinding (source).For(s => s.SelectionChangedCommand).To<ImageCollectionViewModel>(vm => vm.ZoomImageCommand).Apply ();
		
			source.SelectedItemChanged += (object sender, System.EventArgs e) => 
			{
				//new Parameters().ImageToDel = table.IndexPathsForSelectedRows[0].Row;
				(ViewModel as ImageCollectionViewModel).ChosenItem = source.SelectedItem as Image;
			};
  			View.AddSubview(table);
			table.ReloadData();
		}
	}

	public class LocationEntitiesView : MvxTableViewController
	{
		public override void ViewDidLoad()
		{
			View = new UIView() { BackgroundColor = UIColor.Black };
			base.ViewDidLoad();
			TableView = new UITableView ();
			var source = new MvxStandardTableViewSource (TableView, "TitleText Name");

			if(source != null&& TableView!= null)
				TableView.Source = source;
			var viewModel = ViewModel as LocationEntitiesViewModel;
			viewModel.UpdateListView ();
			this.CreateBinding(source).To<LocationEntitiesViewModel>(vm => vm.LocationEntity).Apply();
		}
	}
	public class MapOmnicView : MvxViewController
	{
		public override void ViewDidLoad()
		{
			View = new UIView() { BackgroundColor = UIColor.Black };
			base.ViewDidLoad();
			var mapView = new MKMapView();
			View = mapView;
			var viewModel = ViewModel as MapOmnicViewModel;
			viewModel.LoadLocations ();
			foreach (var item in viewModel.LocationEntity) 
			{
				if (item.Latitude.HasValue && item.Longitude.HasValue) {
					var annotation = new BasicMapAnnotation (new CLLocationCoordinate2D (item.Latitude.Value, item.Longitude.Value), item.Name);
					mapView.AddAnnotation (annotation);
				}
			}
			mapView.ShowsUserLocation = true;
			mapView.DidUpdateUserLocation += (sender, e) => {
				if (mapView.UserLocation != null) {
					CLLocationCoordinate2D coords = mapView.UserLocation.Coordinate;
					MKCoordinateSpan span = new MKCoordinateSpan(2, 2);
					mapView.Region = new MKCoordinateRegion(coords, span);
				}
			};
		}
	}

	class BasicMapAnnotation : MKAnnotation
	{
		public override CLLocationCoordinate2D Coordinate {get;set;}
		string title;
		public override string Title { get{ return title; }}
		public BasicMapAnnotation (CLLocationCoordinate2D coordinate, string title) {
			this.Coordinate = coordinate;
			this.title = title;
		}
	}
	public class GrandChildView : MvxViewController
	{
		public override void ViewDidLoad()
		{
			View = new UIView() { BackgroundColor = UIColor.Black };
			base.ViewDidLoad();
			var imageView = new UIImageView (new RectangleF(0,0, UIScreen.Screens[0].ApplicationFrame.Width, UIScreen.Screens[0].ApplicationFrame.Height));
			imageView.BackgroundColor = UIColor.Red;
				var nsData = NSData.FromUrl (NSUrl.FromString (((ViewModel as GrandChildViewModel).ImageUrl)));

				var image = UIImage.LoadFromData (nsData);
		
				Add (imageView);
				imageView.Image = image;
			//this.CreateBinding (imageView.Image).To<GrandChildViewModel> (vm => vm.ImageUrl).Apply ();
		}
	}
	public class LocationEntityDetailsView : MvxViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}
	}
*/}