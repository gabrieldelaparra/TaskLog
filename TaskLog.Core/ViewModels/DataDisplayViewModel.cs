using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TaskLog.Core.Services.Data;
using TaskLog.Core.Services.Navigation;

namespace TaskLog.Core.ViewModels
{
    public class DataDisplayViewModel : MvxViewModel
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IMvxNavigationService _mvxNavigationService;
        private MvxViewModel _activeViewModel = new WorkWeekViewModel();

        public MvxViewModel ActiveViewModel
        {
            get => _activeViewModel;
            set
            {
                _activeViewModel = value;
                SetProperty(ref _activeViewModel, value);
            }
        }

        public DataDisplayViewModel(IDataService dataService, INavigationService navigationService, IMvxNavigationService mvxNavigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _mvxNavigationService = mvxNavigationService;

            // TODO: Not sure about what is a better architecture. Check if this should be here or in parent (HomeViewModel). 
            _navigationService.OnNavigationDateChange = HandleNavigationDateChange;
            _navigationService.OnNavigationTypeChange = HandleNavigationTypeChange;

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
                        ActivateWeekViewModel(works.Select(x=>new WorkViewModel(x)));
                        break;
                    }
                case NavigationType.Month:
                    {
                        var works = _dataService.GetMonthWorks(_navigationService.NavigationDate);
                        ActivateMonthViewModel(works.Select(x => new WorkViewModel(x)));
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ActivateWeekViewModel(IEnumerable<WorkViewModel> workViewModels)
        {
            var viewModel = Mvx.IoCProvider.Resolve<WorkWeekViewModel>();
            viewModel?.UpdateWorks(workViewModels);
            ActiveViewModel = viewModel;
        }

        public void ActivateMonthViewModel(IEnumerable<WorkViewModel> workViewModels)
        {
            var viewModel = Mvx.IoCProvider.Resolve<WorkViewModel>();
            //viewModel?.UpdateTaskInstances(workViewModels);
            ActiveViewModel = viewModel;
        }
    }
}
