using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using OmnicTabs.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace OmnicTab.Touch
{
	public class GrandChildView : MvxViewController
	{
		public override void ViewDidLoad()
		{
			View = new UIView { BackgroundColor = UIColor.Black };
			base.ViewDidLoad();
			var imageView = new UIImageView (new RectangleF(0,0, UIScreen.Screens[0].ApplicationFrame.Width, UIScreen.Screens[0].ApplicationFrame.Height));
			imageView.BackgroundColor = UIColor.Red;
		    var grandChildViewModel = ViewModel as GrandChildViewModel;
		    if (grandChildViewModel != null)
		    {
		        var nsData = NSData.FromUrl (NSUrl.FromString ((grandChildViewModel.ImageUrl)));

		        var image = UIImage.LoadFromData (nsData);

		        //Add (imageView);
		        var saveButton = UIButton.FromType (UIButtonType.RoundedRect);
		        saveButton.Frame = new RectangleF(0,75,0,25);
		        saveButton.SetTitle("Save to Gallery", UIControlState.Normal);
		        saveButton.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;


		        var removeButton = UIButton.FromType (UIButtonType.RoundedRect);
		        removeButton.Frame = new RectangleF(0,100,0,25);
		        removeButton.SetTitle("Remove", UIControlState.Normal);
		        removeButton.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

		        View.AddSubview (saveButton);
		        View.AddSubview (removeButton);

		        imageView.Image = image;
		    }
		    View.AddSubview (imageView);

			this.CreateBinding (imageView.Image).To<GrandChildViewModel> (vm => vm.ImageUrl).Apply ();
		}
	}
}

