using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TaskLog.Core.ViewModels
{
    public class DayViewModel
    {
        private ObservableCollection<TaskInstanceViewModel> _taskInstances =
                new ObservableCollection<TaskInstanceViewModel>();

        public ObservableCollection<TaskInstanceViewModel> TaskInstances
        {
            get { return _taskInstances; }
            set { _taskInstances = value; }
        }

        public double DayHours => TaskInstances.Sum(x => x.Hours);

    }
}
