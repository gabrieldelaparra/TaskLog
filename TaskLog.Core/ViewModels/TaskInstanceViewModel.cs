using System;
using MvvmCross.ViewModels;
using TaskLog.Core.Models;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.ViewModels
{
    public class TaskInstanceViewModel : MvxViewModel
    {
        public Guid Id { get; set; }
        private DateTime _date;
        private double _hours;
        private string _details;
        private TaskInstanceType _taskInstanceType = TaskInstanceType.Normal;
        private TaskInstance TaskInstance { get; set; }

        public TaskInstanceViewModel() { }

        public void ReadFromModel(TaskInstance taskInstance)
        {
            TaskInstance = taskInstance;
            Date = TaskInstance.Date;
            Hours = TaskInstance.Hours;
            Details = TaskInstance.Details;
            TaskInstanceType = TaskInstance.TaskInstanceType;
        }

        public TaskInstance WriteToModel()
        {
            TaskInstance.Hours = Hours;
            TaskInstance.Date = Date;
            TaskInstance.Details = Details;
            TaskInstance.TaskInstanceType = TaskInstanceType;
            return TaskInstance;
        }

        public void CycleTaskInstanceType() {
            TaskInstanceType = TaskInstanceType.Cycle();
        }

        public Action<TaskInstanceViewModel, DateTime, DateTime> OnNotifyDateChanged { get; set; }
        public Action<TaskInstanceViewModel> OnNotifyHoursChanged { get; set; }
        public Action<TaskInstanceViewModel> OnNotifyTaskInstanceTypeChanged { get; set; }
        public Action<TaskInstanceViewModel> OnNotifyDetailsProjectChanged { get; set; }

        public DateTime Date
        {
            get => _date;
            set {
                var oldDate = _date;
                var newDate = value;
                _date = value;
                SetProperty(ref _date, value);
                OnNotifyDateChanged?.Invoke(this, oldDate, newDate);
            }
        }

        public double Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                SetProperty(ref _hours, value);
                OnNotifyHoursChanged?.Invoke(this);
            }
        }

        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                SetProperty(ref _details, value);
                OnNotifyDetailsProjectChanged?.Invoke(this);
            }
        }

        public TaskInstanceType TaskInstanceType
        {
            get => _taskInstanceType;
            set
            {
                _taskInstanceType = value;
                SetProperty(ref _taskInstanceType, value);
                OnNotifyTaskInstanceTypeChanged?.Invoke(this);
            }
        }
       
    }
}
