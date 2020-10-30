using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TaskLog.Core.ViewModels
{
    public class TaskInstanceWeekViewModel
    {
        private readonly IList<TaskInstanceViewModel> _taskInstances = new List<TaskInstanceViewModel>();
        public ObservableCollection<TaskInstanceDayViewModel> Days { get; private set; } = new ObservableCollection<TaskInstanceDayViewModel>();

        public void UpdateTaskInstances(IEnumerable<TaskInstanceViewModel> taskInstances)
        {
            //Clear the collection
            Days.Clear();
            _taskInstances.Clear();

            //Update the taskInstance for this week (to calculate SumHours)
            foreach (var taskInstanceViewModel in taskInstances)
            {
                _taskInstances.Add(taskInstanceViewModel);
                taskInstanceViewModel.OnNotifyHoursChanged = HandleNotifyHoursChanged;
            }

            //Update the days (to display in the view)
            var groupedByDay = taskInstances.GroupBy(x => x.Date.Date);
            foreach (var dayGroup in groupedByDay)
            {
                var taskInstanceDayViewModel = new TaskInstanceDayViewModel(dayGroup.Key, dayGroup);
                Days.Add(taskInstanceDayViewModel);
            }
            //NotifyPropertyChanged(()=>nameof(SumHours));
            //NotifyPropertyChanged(()=>nameof(IsValidSumHours));
        }

        private void HandleNotifyHoursChanged(TaskInstanceViewModel obj)
        {
            //NotifyPropertyChanged(()=>nameof(SumHours));
            //NotifyPropertyChanged(()=>nameof(IsValidSumHours));
        }

        public double SumHours => _taskInstances.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 40.0;
    }
}
