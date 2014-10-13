using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Util;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using OmnicTabs.Core.ViewModels;
using OmnicTabs.Core.BusinessLayer;

namespace OmnicTabs.Droid.Views
{
    [Activity(Label = "View for MapOmnicViewModel")]
    public class MapOmnicView : MvxFragmentActivity, ILocationListener 
    {
        private Marker _curentLocation;
        private SupportMapFragment _mapFragment;
        private MapOmnicViewModel _viewModel;
        private double _lt;
        private LocationManager _locMgr;
        public double Lt
        {
            get { return _lt; }
            set
            {
                _lt = value;
            }
        }
        private double _lng;
        public double Lng
        {
            get { return _lng; }
            set
            {
                _lng = value;
            }
        }
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Child3View);
            _viewModel = (MapOmnicViewModel)ViewModel;
            Init();

            var set = this.CreateBindingSet<MapOmnicView, MapOmnicViewModel>();
            set.Bind(this)
               .For(m => m.Lt)
               .To(vm => vm.Lt);
            set.Apply();

            var set2 = this.CreateBindingSet<MapOmnicView, MapOmnicViewModel>();
            set.Bind(this)
               .For(m => m.Lng)
               .To(vm => vm.Lng);
            set2.Apply();

            _mapFragment.Map.MapLongClick += Map_MapLongClick;

            _locMgr = GetSystemService(LocationService) as LocationManager;
            var locationCriteria = new Criteria();

            locationCriteria.Accuracy = Accuracy.Coarse;
            locationCriteria.PowerRequirement = Power.Medium;

            if (_locMgr != null)
            {
                var locationProvider = _locMgr.GetBestProvider(locationCriteria, true);

                if (locationProvider != null)
                {
                    _locMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);
                }
                else
                {
                    Log.Info("GPS", "No location providers available");
                }
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            const string provider = LocationManager.GpsProvider;
            if (_locMgr != null && _locMgr.IsProviderEnabled(provider))
            {
                _locMgr.RequestLocationUpdates(provider, 2000, 1, this);
            }
            else
            {
                Log.Info("GPS", " is not available. Does the device have location services enabled?");
            }
           _viewModel.OnPause();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locMgr.RemoveUpdates(this);
            _viewModel.OnPause();

        }

        void Map_MapLongClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            MarkerFactory(e.P0);
            _viewModel.LocationEntity.Add(new LocationEntity { Latitude = e.P0.Latitude, Longitude = e.P0.Longitude });

        }

        Marker MarkerFactory(LatLng latLan, string name = "None")
        {
            var option = new MarkerOptions();
            option.SetPosition(latLan);
            option.SetTitle(name);
            return _mapFragment.Map.AddMarker(option);
        }

        void Init()
        {
            _mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            _mapFragment.Map.Clear();
            foreach (var location in _viewModel.LocationEntity)
            {
                if (location.Latitude.HasValue && location.Longitude.HasValue)
                {
                    MarkerFactory(new LatLng(location.Latitude.Value, location.Longitude.Value), location.Name);
                }
            }
            _curentLocation = MarkerFactory(new LatLng(_viewModel.Lt, _viewModel.Lng), "Current location");

        }

        public void OnLocationChanged(Location location)
        {
            if (_curentLocation != null)
                _curentLocation.Remove();
            _curentLocation = MarkerFactory(new LatLng(_viewModel.Lt, _viewModel.Lng), "Current location");
        }

        public void OnProviderDisabled(string provider)
        {
           // throw new System.NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
           // throw new System.NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
          //  throw new System.NotImplementedException();
        }
    }
}