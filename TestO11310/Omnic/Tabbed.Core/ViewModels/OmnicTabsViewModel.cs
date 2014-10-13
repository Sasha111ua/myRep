using Cirrious.MvvmCross.ViewModels;
using OmnicTabs.Core.BusinessLayer;
using Cirrious.MvvmCross.Plugins.Location;

namespace OmnicTabs.Core.ViewModels
{
    public class OmnicTabsViewModel 
		: MvxViewModel
    {
		public OmnicTabsViewModel(IMvxLocationWatcher watcher)
		{
			ImageCollection = new ImageCollectionViewModel ();
			LocationEntities = new LocationEntitiesViewModel ();
			MapOmnic = new MapOmnicViewModel (watcher);
		}

        private ImageCollectionViewModel _imageCollection;
        public ImageCollectionViewModel ImageCollection
        {
            get { return _imageCollection; }
            set { _imageCollection = value; RaisePropertyChanged(() => ImageCollection); }
        }

        private LocationEntitiesViewModel _locationEntities;
        public LocationEntitiesViewModel LocationEntities
        {
            get { return _locationEntities; }
            set { _locationEntities = value; RaisePropertyChanged(() => LocationEntities); }
        }

        private MapOmnicViewModel _mapOmnic;
        public MapOmnicViewModel MapOmnic
        {
            get { return _mapOmnic; }
            set { _mapOmnic = value; RaisePropertyChanged(() => MapOmnic); }
        }
    }
	public class OmnicTabsTouchViewModel
		: MvxViewModel
	{

		public OmnicTabsTouchViewModel()
		{
			ImageCollection = new ImageCollectionViewModel ();
			LocationEntities = new LocationEntitiesViewModel ();
			MapOmnic = new MapOmnicViewModel ();
		}

		private ImageCollectionViewModel _imageCollection;
		public ImageCollectionViewModel ImageCollection
		{
			get { return _imageCollection; }
			set { _imageCollection = value; RaisePropertyChanged(() => ImageCollection); }
		}

		private LocationEntitiesViewModel _locationEntities;
		public LocationEntitiesViewModel LocationEntities
		{
			get { return _locationEntities; }
			set { _locationEntities = value; RaisePropertyChanged(() => LocationEntities); }
		}

		private MapOmnicViewModel _mapOmnic;
		public MapOmnicViewModel MapOmnic
		{
			get { return _mapOmnic; }
			set { _mapOmnic = value; RaisePropertyChanged(() => MapOmnic); }
		}
	}

    public class Parameters
    {

        private static LocationEntityManager _locationEntityManager;
        public static LocationEntityManager LocationEntityManager
        {
            get { return _locationEntityManager; }
            set { _locationEntityManager = value; }
        }

      /*  private static LocationEntity _locationEntity;
        public static LocationEntity LocationEntity
        {
            get { return  _locationEntity; }
            set { _locationEntity = value; }
        }*/
    }
}
