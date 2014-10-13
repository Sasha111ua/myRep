using Cirrious.CrossCore.IoC;
using OmnicTabs.Core.ViewModels;

namespace OmnicTabs.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
            RegisterAppStart<OmnicTabsViewModel>();
        }
    }
	public class AppTouch : Cirrious.MvvmCross.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			RegisterAppStart<OmnicTabsTouchViewModel>();
		}
	}
}