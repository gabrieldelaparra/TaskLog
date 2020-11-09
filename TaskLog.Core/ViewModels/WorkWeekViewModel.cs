using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.ViewModels;

namespace TaskLog.Core.ViewModels
{
    public class WorkWeekViewModel: MvxViewModel, IWorkCollection
    {
        private readonly IList<WorkViewModel> _works = new List<WorkViewModel>();
        public ObservableCollection<WorkDayViewModel> Days { get; private set; } = new ObservableCollection<WorkDayViewModel>();
        public string TestString { get; set; } = "Test: WorkWeekViewModel";
        public string Header { get; set; }
        public void UpdateWorks(IEnumerable<WorkViewModel> workViewModels) {
            if (!workViewModels.Any()) 
                return;
            //Clear the collection
            Days.Clear();
            _works.Clear();

            //Update the taskInstance for this week (to calculate SumHours)
            foreach (var workViewModel in workViewModels)
            {
                _works.Add(workViewModel);
                workViewModel.OnNotifyHoursChanged = HandleNotifyHoursChanged;
                workViewModel.OnNotifyDateChanged = HandleNotifyDateChanged;
            }

            //Update the days (to display in the view)
            var groupedByDay = workViewModels.GroupBy(x => x.Date.Date).OrderBy(x => x);
            foreach (var dayGroup in groupedByDay)
            {
                var dayCollectionViewModel = new WorkDayViewModel();
                dayCollectionViewModel.Date = dayGroup.Key;
                dayCollectionViewModel.UpdateWorks(dayGroup);
                Days.Add(dayCollectionViewModel);
            }

            var firstDay = groupedByDay.FirstOrDefault().Key;
            var lastDay = groupedByDay.LastOrDefault().Key;
            Header = $"{firstDay.ToShortDateString()} - {lastDay.ToShortDateString()}";

            //Notify hours changed for the week;
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
            RaisePropertyChanged(() => Header);
        }

        private void HandleNotifyDateChanged(WorkViewModel task, DateTime oldDate, DateTime newDate)
        {
            //Filter and update oldDate and newDate Days (if available);
            var affectedDays = Days.Where(x => x.Date.Equals(oldDate) || x.Date.Equals(newDate));
            foreach (var affectedDay in affectedDays) {
                affectedDay.UpdateWorks(_works.Where(x => x.Date.Equals(affectedDay.Date)));
                affectedDay.Date = affectedDay.Date;
            }
        }

        private void HandleNotifyHoursChanged(WorkViewModel obj)
        {
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        public double SumHours => _works.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 40.0;
    }
}
