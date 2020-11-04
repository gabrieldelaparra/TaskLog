using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using MvvmCross.ViewModels;

namespace TaskLog.Core.ViewModels
{
    public class WorkDayViewModel : MvxViewModel, IWorkCollection
    {
        private DateTime _date;

        public ObservableCollection<WorkViewModel> TaskInstances { get; private set; } = new ObservableCollection<WorkViewModel>();
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                SetProperty(ref _date, value);
            }
        }

        public void UpdateTaskInstances(IEnumerable<WorkViewModel> taskInstances)
        {
            TaskInstances.Clear();
            foreach (var taskInstanceViewModel in taskInstances)
            {
                TaskInstances.Add(taskInstanceViewModel);
                taskInstanceViewModel.OnNotifyHoursChanged = HandleNotifyHoursChanged;
            }

            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        private void HandleNotifyHoursChanged(WorkViewModel task)
        {
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        public double SumHours => TaskInstances.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 8.0;

    }
}
