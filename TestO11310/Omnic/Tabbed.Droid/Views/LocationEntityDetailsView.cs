using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace OmnicTabs.Droid.Views
{
    [Activity(Label = "View for LocationEntityDetailsViewModel")]
    public class LocationEntityDetails : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LocationEntityDetails);
        }
    }
}