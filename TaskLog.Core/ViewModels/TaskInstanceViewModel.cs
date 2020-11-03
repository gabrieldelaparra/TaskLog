using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskLog.Core.Annotations;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class TaskInstanceViewModel : INotifyPropertyChanged
    {
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

        public Action<TaskInstanceViewModel> NotifyDateChanged { get; set; }
        public Action<TaskInstanceViewModel> NotifyHoursChanged { get; set; }
        public Action<TaskInstanceViewModel> NotifyTaskInstanceTypeChanged { get; set; }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
                //SetProperty(ref _date, value);
                NotifyDateChanged?.Invoke(this);
            }
        }

        public double Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
                //SetProperty(ref _hours, value);
                NotifyHoursChanged?.Invoke(this);
            }
        }

        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                //SetProperty(ref _details, value);
                OnPropertyChanged(nameof(Details));
            }
        }

        public TaskInstanceType TaskInstanceType
        {
            get => _taskInstanceType;
            set
            {
                _taskInstanceType = value;
                OnPropertyChanged(nameof(TaskInstanceType));
                //SetProperty(ref _taskInstanceType, value);
                NotifyTaskInstanceTypeChanged?.Invoke(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
