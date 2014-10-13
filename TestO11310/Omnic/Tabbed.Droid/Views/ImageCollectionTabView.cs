using Android.Content.Res;
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.Views;
using OmnicTabs.Core.ViewModels;
using Android.App;

namespace OmnicTabs.Droid.Views
{
    [Activity(Label = "View for ImageCollectionViewModel")]
    public class ImageCollectionView : MvxActivity
    {
        private MvxListView _listView;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Child1View);
            _listView = FindViewById<MvxListView>(Resource.Id.list_view);
            (_listView as ListView).ItemClick += _listView_ItemClick;
        }

        void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e != null)
            {
                var viewModel = (ImageCollectionViewModel) ViewModel;
                viewModel.ImageToDeletePosition = e.Position;
                viewModel.ShowDetails();
            }
        }
    }
}