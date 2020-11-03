using System;
using System.Collections.Generic;
using MvvmCross.ViewModels;
using TaskLog.Core.Services;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.ViewModels
{
    public class NavigationViewModel : MvxViewModel
    {
        private readonly IDataService _dataService;
        private DateTime _navigationDate = DateTime.Today;
        private NavigationType _navigationType = NavigationType.Week;
        private IEnumerable<WorkViewModel> _works;
        public WorkWeekViewModel WorkWeekViewModel { get; } = new WorkWeekViewModel();
        private string WeekPeriodLabel =>
            $"Week: {_navigationDate.StartOfWeek().ToShortDateString()} to {_navigationDate.StartOfWeek().AddDays(5).ToShortDateString()}";
        private string MonthPeriodLabel => $"Month: {_navigationDate.Month}";

        public NavigationViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }
        public override void Prepare()
        {
            LoadData();
        }
        public void LoadData()
        {
            switch (_navigationType)
            {
                case NavigationType.Week:
                    {
                        _works = _dataService.LoadWeekWorks(_navigationDate);
                        WorkWeekViewModel.UpdateTaskInstances(_works);
                        //Activate<WorkWeekViewModel>(WorkWeekViewModel);
                        break;
                    }
                case NavigationType.Month:
                    {
                        _works = _dataService.LoadMonthWorks(_navigationDate);
                        //WorkWeekViewModel.UpdateTaskInstances(_works);
                        //Activate<WorkWeekViewModel>(WorkWeekViewModel);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            RaisePropertyChanged(() => NavigationPeriod);
        }

        public string NavigationPeriod
        {
            get
            {
                return _navigationType switch
                {
                    NavigationType.Week => WeekPeriodLabel,
                    NavigationType.Month => MonthPeriodLabel,
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
        }

        public void ChangeNavigationToWeek()
        {
            if (_navigationType == NavigationType.Week) return;
            _navigationType = NavigationType.Week;
            LoadData();
        }

        public void ChangeNavigationToMonth()
        {
            if (_navigationType == NavigationType.Month) return;
            _navigationType = NavigationType.Month;
            LoadData();
        }

        public void NavigateNext()
        {
            _navigationDate = _navigationType switch
            {
                NavigationType.Week => _navigationDate.AddDays(7),
                NavigationType.Month => _navigationDate.AddMonths(1),
                _ => throw new ArgumentOutOfRangeException(),
            };
            LoadData();
        }

        public void NavigatePrevious()
        {
            _navigationDate = _navigationType switch
            {
                NavigationType.Week => _navigationDate.AddDays(-7),
                NavigationType.Month => _navigationDate.AddMonths(-1),
                _ => throw new ArgumentOutOfRangeException(),
            };
            LoadData();
        }

        public void NavigateToToday()
        {
            _navigationDate = DateTime.Today;
            LoadData();
        }
    }
}
