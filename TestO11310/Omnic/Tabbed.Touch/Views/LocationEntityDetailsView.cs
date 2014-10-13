
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using OmnicTabs.Core.ViewModels;

namespace Tabbed.Touch
{
	public partial class LocationEntityDetailsView : MvxViewController
	{
		public LocationEntityDetailsView () : base ("LocationEntityDetailsView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.CreateBinding(NameTextField).To((LocationEntityDetailsViewModel vm) => vm.LocationEntity.Name).Apply();
			this.CreateBinding(LatitudeTextField).To((LocationEntityDetailsViewModel vm) => vm.Latitude).Apply();
			this.CreateBinding(LongitudeTextField).To((LocationEntityDetailsViewModel vm) => vm.Longitude).Apply();

			this.CreateBinding(SaveButton).To((LocationEntityDetailsViewModel vm) => vm.SaveCommand).Apply();
			this.CreateBinding(CancelButton).To((LocationEntityDetailsViewModel vm) => vm.CancelCommand).Apply();
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

