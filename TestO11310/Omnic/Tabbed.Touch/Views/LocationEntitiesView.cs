using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using OmnicTabs.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.Foundation;
using MonoTouch.Dialog;
using OmnicTabs.Core.BusinessLayer;

namespace OmnicTab.Touch
{
	public class LocationEntitiesView : MvxTableViewController
	{
		LocationEntitiesViewModel _viewModel;
		public override void ViewDidLoad()
		{
			View = new UIView() { BackgroundColor = UIColor.Black };
			base.ViewDidLoad();
			TableView = new UITableView ();
			var source = new LocationTableSource (TableView, "TitleText Name");
			TableView.Source = source;
			_viewModel = ViewModel as LocationEntitiesViewModel;
		    if (_viewModel != null) _viewModel.UpdateListView ();
		    this.CreateBinding(source).To<LocationEntitiesViewModel>(vm => vm.LocationEntity).Apply();

			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, (o,e)=> ((LocationEntitiesViewModel)ViewModel).ShowDetails(0));
			//this.CreateBinding(addButton).To<LocationEntitiesViewModel>(vm => vm.AddCommand).Apply();

			source.SelectedItemChanged += (sender, e) => {
				var selectedItem = (source.SelectedItem as LocationEntity);
				((LocationEntitiesViewModel)ViewModel).ShowDetails(selectedItem.Id);
			};

			NavigationItem.RightBarButtonItem = addButton;

		}
	}

	public class LocationTableSource : MvxStandardTableViewSource
	{
		UITableView _table;
		public LocationTableSource (UITableView table, string bind): base(table, bind)
		{
			_table = table;
		}
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				// remove the item from the underlying data source
				//_table.Source.tableItems.RemoveAt(indexPath.Row);
				// delete the row from the table
				tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				break;
			case UITableViewCellEditingStyle.None:
				Console.WriteLine ("CommitEditingStyle:None called");
				break;
			}
		}
	}
}

