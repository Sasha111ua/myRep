using Android.App;
using Android.OS;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Plugins.Location;
using OmnicTabs.Core.ViewModels;

namespace OmnicTabs.Droid.Views
{
    [Activity (ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class OmnicTabsView : MvxTabActivity
    {
        protected OmnicTabsViewModel FirstViewModel
        {
            get
            {
               if (ViewModel != null) 
                    return ViewModel as OmnicTabsViewModel;
                return new OmnicTabsViewModel(Mvx.Resolve<IMvxLocationWatcher>());
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.OmnicTabsView);
                TabHost.TabSpec spec = TabHost.NewTabSpec("child1");
                spec.SetIndicator("Images");
                spec.SetContent(this.CreateIntentFor(FirstViewModel.ImageCollection));
                TabHost.AddTab(spec);

                spec = TabHost.NewTabSpec("child2");
                spec.SetIndicator("Location DB");
                spec.SetContent(this.CreateIntentFor(FirstViewModel.LocationEntities));
                TabHost.AddTab(spec);

                spec = TabHost.NewTabSpec("child3");
                spec.SetIndicator("Map");
                spec.SetContent(this.CreateIntentFor(FirstViewModel.MapOmnic));
                TabHost.AddTab(spec);
        }
    }
}