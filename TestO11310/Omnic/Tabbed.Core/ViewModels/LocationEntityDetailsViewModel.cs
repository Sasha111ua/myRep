using Cirrious.MvvmCross.ViewModels;
using OmnicTabs.Core.BusinessLayer;
using System;
using System.Windows.Input;

namespace OmnicTabs.Core.ViewModels
{
    public class LocationEntityDetailsViewModel
        : MvxViewModel
    {
        public LocationEntityDetailsViewModel()
        {
                
        }
        public void Init(int id)
        {
            _locationEntity = id != 0 ? Parameters.LocationEntityManager.GetItem(id) : new LocationEntity();
        }

        private LocationEntity _locationEntity;
        public LocationEntity LocationEntity
        {
            get { return _locationEntity; }
            set { _locationEntity = value; RaisePropertyChanged(() => LocationEntity); }
        }

        public string Longitude
        {
            get { return LocationEntity.Longitude.HasValue ? LocationEntity.Longitude.Value.ToString() : String.Empty; }
            set { LocationEntity.Longitude = Convert.ToDouble(value); RaisePropertyChanged(() => Longitude); }
        }

        public string Latitude
        {
            get { return LocationEntity.Latitude.ToString(); }
            set
            {
                if (value == "")
                    LocationEntity.Latitude = null;
                else if (value == "-")
                    LocationEntity.Latitude = -0;
                else
                    LocationEntity.Latitude = Convert.ToDouble(value);
            }
        }
        public DateTime TimeUpdated
        {
            get { return LocationEntity.TimeUpdated; }
            set { LocationEntity.TimeUpdated = value; RaisePropertyChanged(() => TimeUpdated); }
        }

        private MvxCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                _cancelCommand = _cancelCommand ?? new MvxCommand(() => Close(this));
                return _cancelCommand;
            }
        }
        private MvxCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new MvxCommand(SaveLocationEntity);
                return _saveCommand;
            }
        }

        void SaveLocationEntity()
        {
            LocationEntity.TimeUpdated = DateTime.Now;
            Parameters.LocationEntityManager.SaveItem(LocationEntity);
            Close(this);
        }
    }
}
