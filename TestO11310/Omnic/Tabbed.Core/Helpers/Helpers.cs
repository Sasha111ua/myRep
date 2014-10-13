using Cirrious.CrossCore;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;

namespace OmnicTabs.Core.Helpers
{
    public class Helpers : MvxViewModel
    {
        public void ShowViewModel<T>(dynamic parameter) where T : IMvxViewModel
        {
            var viewDispatcher = Mvx.Resolve<IMvxViewDispatcher>();
            var request = MvxViewModelRequest.GetDefaultRequest(typeof(T));
            request.ParameterValues = ((object)parameter).ToSimplePropertyDictionary();
            viewDispatcher.ShowViewModel(request);
        }
    }
}
