using System;
using System.Collections.Generic;
using MvvmCross.ViewModels;
using TaskLog.Core.Services;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.ViewModels
{
    public class WorkWeekNavigationViewModel : MvxViewModel
    {
        private DateTime _navigationDate = DateTime.Today;

        public string NavigationWeek =>
            $"Week: {_navigationDate.StartOfWeek().ToShortDateString()} to {_navigationDate.StartOfWeek().AddDays(5).ToShortDateString()}";
        public WorkWeekViewModel WeekCollectionViewModel { get; } = new WorkWeekViewModel();
        private IEnumerable<WorkViewModel> _taskInstances;
        private readonly IDataService _dataService;
        public WorkWeekNavigationViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }
        public override void Prepare()
        {
            LoadData();
        }

        public void LoadData()
        {
            _taskInstances = _dataService.LoadWeekWorks(_navigationDate);
            WeekCollectionViewModel.UpdateTaskInstances(_taskInstances);
            RaisePropertyChanged(() => NavigationWeek);
        }


        public void NavigateNext()
        {
            _navigationDate = _navigationDate.AddDays(7);
            LoadData();
        }

        public void NavigatePrevious()
        {
            _navigationDate = _navigationDate.AddDays(-7);
            LoadData();
        }

        public void NavigateToToday()
        {
            _navigationDate = DateTime.Today;
            LoadData();
        }
    }
}
