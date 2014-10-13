using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;

namespace OmnicTabs.Core.ViewModels
{
    public class GrandChildViewModel
        : MvxViewModel
    {
        string _imageUrl;

        public void Init(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; RaisePropertyChanged(() => ImageUrl); }
        }
        private MvxCommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                _deleteCommand = _deleteCommand ?? new MvxCommand(ImageCollectionViewModel.DeleteImage);
                return _deleteCommand;
            }
        }
    }
}
