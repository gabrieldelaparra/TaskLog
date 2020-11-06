using MvvmCross.ViewModels;

namespace TaskLog.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        public NavigationViewModel NavigationViewModel { get; set; }
        public DataDisplayViewModel DataDisplayViewModel { get; set; }
        public HomeViewModel(NavigationViewModel navigationViewModel, DataDisplayViewModel dataDisplayViewModel)
        {
            NavigationViewModel = navigationViewModel;
            DataDisplayViewModel = dataDisplayViewModel;
        }
    }
}
