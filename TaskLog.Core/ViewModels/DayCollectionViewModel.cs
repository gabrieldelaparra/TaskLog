using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using MvvmCross.ViewModels;

namespace TaskLog.Core.ViewModels
{
    public class DayCollectionViewModel : MvxViewModel
    {
        private DateTime _date;

        public ObservableCollection<TaskInstanceViewModel> TaskInstances { get; private set; } = new ObservableCollection<TaskInstanceViewModel>();
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                SetProperty(ref _date, value);
            }
        }

        public void UpdateTaskInstances(DateTime date, IEnumerable<TaskInstanceViewModel> taskInstances)
        {
            Date = date;

            TaskInstances.Clear();
            foreach (var taskInstanceViewModel in taskInstances)
            {
                TaskInstances.Add(taskInstanceViewModel);
                taskInstanceViewModel.OnNotifyHoursChanged = HandleNotifyHoursChanged;
            }

            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        private void HandleNotifyHoursChanged(TaskInstanceViewModel task)
        {
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        public double SumHours => TaskInstances.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 8.0;

    }
}
