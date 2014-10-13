using Cirrious.MvvmCross.ViewModels;
using OmnicTabs.Core.BusinessLayer;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace OmnicTabs.Core.ViewModels
{
    public class LocationEntitiesViewModel
    : MvxViewModel
    {
        public void UpdateListView()
        {
            LocationEntity = new ObservableCollection<LocationEntity>(Parameters.LocationEntityManager.GetItems().ToList());
        }

        private ObservableCollection<LocationEntity> _locationEntity;
        public ObservableCollection<LocationEntity> LocationEntity
        {
            get { return _locationEntity; }
            set { _locationEntity = value; RaisePropertyChanged(() => LocationEntity); }
        }
       
        public void ShowDetails(int id)
        {
            new Helpers.Helpers().ShowViewModel<LocationEntityDetailsViewModel>(new {id = id});
        }

        public ICommand AddCommand
        {
            get
            {
                return new MvxCommand(
					() =>  new Helpers.Helpers().ShowViewModel<LocationEntityDetailsViewModel>(new {id = 0}));
            }
        }
    }
}
