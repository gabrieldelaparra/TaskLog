using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using TaskLog.Core.Services.Data;
using TaskLog.Core.Services.DataLoader;
using TaskLog.Core.Services.Navigation;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IFileConfiguration, JsonFileConfiguration>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDataLoaderService, JsonDataLoaderService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDataService, InMemoryDataService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<INavigationService, NavigationService>();
            // I think that singletons would do for both the Navigation and DataDisplay
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<NavigationViewModel, NavigationViewModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<DataDisplayViewModel, DataDisplayViewModel>();

            Mvx.IoCProvider.RegisterType<WorkWeekViewModel>();
            Mvx.IoCProvider.RegisterType<WorkDayViewModel>();
            Mvx.IoCProvider.RegisterType<WorkViewModel>();
            Mvx.IoCProvider.RegisterType<ProjectViewModel>();
            Mvx.IoCProvider.RegisterType<TaskViewModel>();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
