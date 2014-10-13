using Cirrious.MvvmCross.Plugins.Location;
using Cirrious.MvvmCross.ViewModels;
using OmnicTabs.Core.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OmnicTabs.Core.ViewModels
{
    public class MapOmnicViewModel
     : MvxViewModel
    {
        private readonly IMvxLocationWatcher _watcher;
        private ObservableCollection<LocationEntity> _locationEntity;
        public ObservableCollection<LocationEntity> LocationEntity
        {
            get { return _locationEntity; }
            set { _locationEntity = value; RaisePropertyChanged(() => LocationEntity); }
        }

        private double _lng;
        public double Lng
        {
            get { return _lng; }
            set { _lng = value; RaisePropertyChanged(() => Lng); }
        }
        private double _lt;
        public double Lt
        {
            get { return _lt; }
            set { _lt = value; RaisePropertyChanged(() => Lt); }
        }
        public MapOmnicViewModel()
        {

        }
        public MapOmnicViewModel(IMvxLocationWatcher watcher)
        {
            _watcher = watcher;
            if(!_watcher.Started)
            _watcher.Start(new MvxLocationOptions(), OnFix, OnError);
            LoadLocations();
        }
        public void OnPause()
        {
            _watcher.Stop();
        }

        public void OnResume()
        {
            LoadLocations();
        }
		public void LoadLocations()
		{
			LocationEntity = new ObservableCollection<LocationEntity>(Parameters.LocationEntityManager.GetItems().ToList());
		}

        private void OnError(MvxLocationError obj)
        {
            //error
        }

        private void OnFix(MvxGeoLocation obj)
        {
            Lt = obj.Coordinates.Latitude;
            Lng = obj.Coordinates.Longitude;
        }
    }
}
