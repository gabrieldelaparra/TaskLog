using System;
using MvvmCross.ViewModels;
using TaskLog.Core.Services;

namespace TaskLog.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        public NavigationViewModel NavigationViewModel { get; set; }
        public DataDisplayViewModel DataDisplayViewModel { get; set; }
        public HomeViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            
            _navigationService = navigationService;
            //TODO: Check if required to move these later to Prepare() or Initialize();
            _navigationService.OnNavigationDateChange = HandleNavigationDateChange;
            _navigationService.OnNavigationTypeChange = HandleNavigationTypeChange;

            ReloadWorks();
        }

        private void ReloadWorks()
        {
            switch (_navigationService.NavigationType)
            {
                case NavigationType.Week:
                    {
                        var works = _dataService.GetWeekWorks(_navigationService.NavigationDate);
                        DataDisplayViewModel.UpdateTaskInstances(works);
                        DataDisplayViewModel.ActivateWeekViewModel();
                        break;
                    }
                case NavigationType.Month:
                    {
                        var works = _dataService.GetMonthWorks(_navigationService.NavigationDate);
                        DataDisplayViewModel.UpdateTaskInstances(works);
                        DataDisplayViewModel.ActivateMonthViewModel();
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void HandleNavigationTypeChange()
        {
            ReloadWorks();
        }

        private void HandleNavigationDateChange()
        {
            ReloadWorks();
        }
    }
}
