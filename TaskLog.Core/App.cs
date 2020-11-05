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
            RegisterAppStart<HomeViewModel>();

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IFileConfiguration, JsonFileConfiguration>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDataLoaderService, JsonDataLoaderService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDataService, InMemoryDataService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<INavigationService, NavigationService>();
        }
    }
}
