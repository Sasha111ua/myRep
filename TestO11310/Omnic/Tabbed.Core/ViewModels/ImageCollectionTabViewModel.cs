using System.Collections.ObjectModel;
using Cirrious.MvvmCross.ViewModels;
using OmnicTabs.Core.Services;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;

namespace OmnicTabs.Core.ViewModels
{
    public class ImageCollectionViewModel
    : MvxViewModel
    {

        Image _chosenItem;
        public Image ChosenItem
        {
            get { return _chosenItem; }
            set
            {
                _chosenItem = value;
                RaisePropertyChanged(() => ChosenItem);
            }
        }

        static ObservableCollection<Image> _images;
        public ObservableCollection<Image> Images
        {
            get { return _images; }
            set { _images = value; RaisePropertyChanged(() => Images); }
        }
        public string Refresh { get { return "Refresh"; } }

        public ImageCollectionViewModel()
        {
            LoadImages(new ImageLoader());
        }

        public void ShowDetails()
        {
            new Helpers.Helpers().ShowViewModel<GrandChildViewModel>(new {imageUrl = ChosenItem.Url});
        }

        public ICommand RefreshCommand
        {
            get { return new MvxCommand(() => LoadImages(new ImageLoader())); }
        }

        private async void LoadImages(IImageService service)
        {
            Images = await Task<ObservableCollection<Image>>.Factory.StartNew(() =>
            {
                var newList = new ObservableCollection<Image>();
                for (var i = 0; i < 5; i++)
                {
                    var newImage = service.ImageFactory();
                    newList.Add(newImage);
                }

                return newList;
            });
        }

        private static int _imageToDeletePosition;
        public int ImageToDeletePosition {
            get { return _imageToDeletePosition; }
            set { _imageToDeletePosition = value; RaisePropertyChanged(()=> ImageToDeletePosition); }
        }
        public static void DeleteImage()
        {
            if (_images.Any())
                _images.RemoveAt(_imageToDeletePosition);
        }
    }
}
