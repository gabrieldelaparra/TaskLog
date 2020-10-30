using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TaskLog.Core.ViewModels
{
    public class TaskInstanceDayViewModel
    {
        //When the collection changes, notify and update the hours;
        public TaskInstanceDayViewModel()
        {
            //I think that I will manually update this in the UpdateCollectionMethod
            //TaskInstances.CollectionChanged += TaskInstancesOnCollectionChanged;
        }

        public TaskInstanceDayViewModel(DateTime date, IEnumerable<TaskInstanceViewModel> taskInstances) {
            Date = date;
            UpdateTaskInstances(taskInstances);
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                //NotifyPropertyChanged(()=>nameof(Date));
            }
        }


        public ObservableCollection<TaskInstanceViewModel> TaskInstances { get; private set; } = new ObservableCollection<TaskInstanceViewModel>();

        /// <summary>
        /// For the week/month updates the viewModels
        /// </summary>
        /// <param name="taskInstances"></param>
        public void UpdateTaskInstances(IEnumerable<TaskInstanceViewModel> taskInstances)
        {
            TaskInstances.Clear();
            foreach (var taskInstanceViewModel in taskInstances)
            {
                taskInstanceViewModel.OnNotifyHoursChanged = HandleNotifyHoursChanged;
                TaskInstances.Add(taskInstanceViewModel);
            }
            //NotifyPropertyChanged(()=>nameof(SumHours));
            //NotifyPropertyChanged(()=>nameof(IsValidSumHours));
        }

        /// <summary>
        /// To calculate the SumHours after any changes;
        /// </summary>
        /// <param name="obj"></param>
        private void HandleNotifyHoursChanged(TaskInstanceViewModel obj)
        {
            //NotifyPropertyChanged(() => nameof(SumHours));
            //NotifyPropertyChanged(()=>nameof(IsValidSumHours));
        }

        public double SumHours => TaskInstances.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 8.0;

    }
}
