using System;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using OmnicTabs.Core.ViewModels;
using MonoTouch.ObjCRuntime;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using OmnicTabs.Core.Services;

namespace OmnicTab.Touch
{
	public class ImageCollectionView : MvxTableViewController
	{
		ImageCollectionViewModel _viewModel;
		public override void ViewDidLoad()
		{
			View = new UIView() { BackgroundColor = UIColor.Clear};
			base.ViewDidLoad();
			_viewModel = (ImageCollectionViewModel)ViewModel;
			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			var refreshButton = new UIBarButtonItem (UIBarButtonSystemItem.Refresh);
			this.CreateBinding (refreshButton).To<ImageCollectionViewModel> (vm => vm.RefreshCommand).Apply ();
			NavigationItem.RightBarButtonItem = refreshButton;


			//table
			TableView = new UITableView();
			var source = new MvxStandardTableViewSource(TableView, "ImageUrl Url");
			TableView.Source = source;
			this.CreateBinding(source).To<ImageCollectionViewModel>(vm => vm.Images).Apply();

			source.SelectedItemChanged += (sender, e) => 
			{
				_viewModel.ImageToDeletePosition = TableView.IndexPathsForSelectedRows[0].Row;
			   /* var imageCollectionViewModel = ViewModel as ImageCollectionViewModel;
			    if (imageCollectionViewModel != null)*/
                    _viewModel.ChosenItem = source.SelectedItem as Image;
			    _viewModel.ShowDetails();
			};

			TableView.ReloadData();
		}
	}
}

