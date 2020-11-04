using System.Collections.Generic;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TaskLog.Core.Services;

namespace TaskLog.Core.ViewModels
{
    public class DataDisplayViewModel : MvxViewModel, IWorkCollection
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IMvxNavigationService _mvxNavigationService;
        private IWorkCollection _activeDataDisplayViewModel;

        public DataDisplayViewModel(IDataService dataService, INavigationService navigationService, IMvxNavigationService mvxNavigationService) {
            _dataService = dataService;
            _navigationService = navigationService;
            _mvxNavigationService = mvxNavigationService;

            //TODO: Not sure if required;
            _activeDataDisplayViewModel = new WorkWeekViewModel();
        }

        public void UpdateTaskInstances(IEnumerable<WorkViewModel> workViewModels) {
            _activeDataDisplayViewModel.UpdateTaskInstances(workViewModels);
        }

        public void ActivateWeekViewModel() {
            _activeDataDisplayViewModel = new WorkWeekViewModel();
            _mvxNavigationService.Navigate(_activeDataDisplayViewModel as WorkWeekViewModel);
        }

        public void ActivateMonthViewModel() {
            //_mvxNavigationService.Navigate(new WorkWeekViewModel());
        }
    }
}
