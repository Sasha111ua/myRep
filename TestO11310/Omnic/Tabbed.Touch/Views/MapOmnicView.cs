using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using OmnicTabs.Core.ViewModels;
using MonoTouch.CoreLocation;

namespace OmnicTab.Touch
{
	public class MapOmnicView : MvxViewController
	{
		public override void ViewDidLoad()
		{
			View = new UIView() { BackgroundColor = UIColor.Black };
			base.ViewDidLoad();
			var mapView = new MKMapView();
			View = mapView;
			var viewModel = ViewModel as MapOmnicViewModel;
		    if (viewModel != null)
		    {
		        viewModel.LoadLocations ();
		        foreach (var item in viewModel.LocationEntity) 
		        {
		            if (item.Latitude.HasValue && item.Longitude.HasValue) {
		                var annotation = new BasicMapAnnotation (new CLLocationCoordinate2D (item.Latitude.Value, item.Longitude.Value), item.Name);
		                mapView.AddAnnotation (annotation);
		            }
		        }
		    }
		    mapView.ShowsUserLocation = true;
			mapView.DidUpdateUserLocation += (sender, e) => {
			                                                    if (mapView.UserLocation == null) return;
			                                                    CLLocationCoordinate2D coords = mapView.UserLocation.Coordinate;
			                                                    var span = new MKCoordinateSpan(2, 2);
			                                                    mapView.Region = new MKCoordinateRegion(coords, span);
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

}

