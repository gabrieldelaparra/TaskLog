using System;
using MvvmCross.ViewModels;
using TaskLog.Core.Models;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.ViewModels
{
    public class WorkViewModel : MvxViewModel
    {
        public Guid Id { get; set; }
        private DateTime _date;
        private double _hours;
        private string _details;
        private WorkType _taskInstanceType = WorkType.Normal;
        private Work Work { get; set; }

        public WorkViewModel() { }

        public WorkViewModel(Work work) {
            ReadFromModel(work);
        }

        public void ReadFromModel(Work work)
        {
            Work = work;
            Date = Work.Date;
            Hours = Work.Hours;
            Details = Work.Details;
            TaskInstanceType = Work.WorkType;
        }

        public Work WriteToModel()
        {
            Work.Hours = Hours;
            Work.Date = Date;
            Work.Details = Details;
            Work.WorkType = TaskInstanceType;
            return Work;
        }

        public void CycleTaskInstanceType() {
            TaskInstanceType = TaskInstanceType.Cycle();
        }

        public Action<WorkViewModel, DateTime, DateTime> OnNotifyDateChanged { get; set; }
        public Action<WorkViewModel> OnNotifyHoursChanged { get; set; }
        public Action<WorkViewModel> OnNotifyTaskInstanceTypeChanged { get; set; }
        public Action<WorkViewModel> OnNotifyDetailsProjectChanged { get; set; }

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

        public WorkType TaskInstanceType
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
