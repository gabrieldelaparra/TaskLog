using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.ViewModels;

namespace TaskLog.Core.ViewModels
{
    public class WorkWeekViewModel: MvxViewModel
    {
        private readonly IList<WorkViewModel> _taskInstances = new List<WorkViewModel>();
        public ObservableCollection<WorkDayViewModel> Days { get; private set; } = new ObservableCollection<WorkDayViewModel>();

        public void UpdateTaskInstances(IEnumerable<WorkViewModel> taskInstances)
        {
            //Clear the collection
            Days.Clear();
            _taskInstances.Clear();

            //Update the taskInstance for this week (to calculate SumHours)
            foreach (var taskInstanceViewModel in taskInstances)
            {
                _taskInstances.Add(taskInstanceViewModel);
                taskInstanceViewModel.OnNotifyHoursChanged = HandleNotifyHoursChanged;
                taskInstanceViewModel.OnNotifyDateChanged = OnNotifyDateChanged;
            }

            //Update the days (to display in the view)
            var groupedByDay = taskInstances.GroupBy(x => x.Date.Date);
            foreach (var dayGroup in groupedByDay)
            {
                var dayCollectionViewModel = new WorkDayViewModel();
                dayCollectionViewModel.UpdateTaskInstances(dayGroup.Key, dayGroup);
                Days.Add(dayCollectionViewModel);
            }

            //Notify hours changed for the week;
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        private void OnNotifyDateChanged(WorkViewModel task, DateTime oldDate, DateTime newDate)
        {
            //Filter and update oldDate and newDate Days (if available);
            var affectedDays = Days.Where(x => x.Date.Equals(oldDate) || x.Date.Equals(newDate));
            foreach (var affectedDay in affectedDays) {
                affectedDay.UpdateTaskInstances(affectedDay.Date,
                    _taskInstances.Where(x => x.Date.Equals(affectedDay.Date)));
            }
        }

        private void HandleNotifyHoursChanged(WorkViewModel obj)
        {
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        public double SumHours => _taskInstances.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 40.0;
    }
}
