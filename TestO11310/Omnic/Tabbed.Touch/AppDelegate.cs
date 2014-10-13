using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Tabbed.Touch;
using OmnicTabs.Core.BusinessLayer;
using System.IO;
using OmnicTabs.DL.SQLite;
using OmnicTabs.Core.ViewModels;
using System;

namespace OmnicTabs.Touch
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        UIWindow _window;
		public static AppDelegate Current { get; private set; }

		public LocationEntityManager LocMgr { get; set; }
		Connection conn;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, _window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();
			var sqliteFilename = "OmnicTabsDB.db3";
			string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(libraryPath, sqliteFilename);
			conn = new Connection(path);

			LocMgr = new LocationEntityManager(conn);
			LocMgr.SaveItem(new LocationEntity() { Name = "Home", Latitude = -5.122416, Longitude = 95.904083, TimeUpdated = DateTime.Now });
			LocMgr.SaveItem(new LocationEntity() { Name = "Home2", Latitude = 51.122416, Longitude = 95.904083, TimeUpdated = DateTime.Now });
			Parameters.LocationEntityManager = LocMgr;
            _window.MakeKeyAndVisible();

            return true;
        }
    }
}