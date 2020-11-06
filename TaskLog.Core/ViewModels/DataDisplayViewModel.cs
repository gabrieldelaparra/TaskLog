using System;
using System.Collections.Generic;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TaskLog.Core.Services;
using TaskLog.Core.Services.Data;
using TaskLog.Core.Services.Navigation;

namespace TaskLog.Core.ViewModels
{
    public class DataDisplayViewModel : MvxViewModel
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IMvxNavigationService _mvxNavigationService;

        public DataDisplayViewModel(IDataService dataService, INavigationService navigationService, IMvxNavigationService mvxNavigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            
            // TODO: Check if this should be here or in Home. Not sure about what is a better architecture.
            _navigationService.OnNavigationDateChange = HandleNavigationDateChange;
            _navigationService.OnNavigationTypeChange = HandleNavigationTypeChange;

            _mvxNavigationService = mvxNavigationService;

            ReloadWorks();
        }

        private void HandleNavigationTypeChange()
        {
            ReloadWorks();
        }

        private void HandleNavigationDateChange()
        {
            ReloadWorks();
        }

        private void ReloadWorks()
        {
            switch (_navigationService.NavigationType)
            {
                case NavigationType.Week:
                    {
                        var works = _dataService.GetWeekWorks(_navigationService.NavigationDate);
                        ActivateWeekViewModel(works);
                        break;
                    }
                case NavigationType.Month:
                    {
                        var works = _dataService.GetMonthWorks(_navigationService.NavigationDate);
                        ActivateMonthViewModel(works);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ActivateWeekViewModel(IEnumerable<WorkViewModel> workViewModels)
        {
            var workWeekViewModel = Mvx.IoCProvider.Resolve<WorkWeekViewModel>();
            workWeekViewModel.UpdateTaskInstances(workViewModels);
            _mvxNavigationService.Navigate(workWeekViewModel);
        }

        public void ActivateMonthViewModel(IEnumerable<WorkViewModel> workViewModels)
        {
            //var workWeekViewModel = Mvx.IoCProvider.Resolve<WorkWeekViewModel>();
            //workWeekViewModel.UpdateTaskInstances(workViewModels);
            //_mvxNavigationService.Navigate(workWeekViewModel);
        }
    }
}
