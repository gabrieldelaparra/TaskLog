using System;
using MvvmCross;
using MvvmCross.ViewModels;
using TaskLog.Core.Models;
using TaskLog.Core.Services.Data;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.ViewModels
{
    public class WorkViewModel : MvxViewModel
    {
        public Guid Id { get; set; }
        private DateTime _date;
        private double _hours;
        private string _details;
        private WorkType _workType = WorkType.Normal;
        private Work Work { get; }
        private TaskViewModel Task { get; set; }
        private ProjectViewModel Project { get; set; }

        public WorkViewModel() { }

        public WorkViewModel(Work work)
        {
            Work = work;
            FromModel(Work);
        }
        public string Header => $"({Hours}) [{WorkType}] {Details}";

        private void FromModel(Work model)
        {
            var dataService = Mvx.IoCProvider.Resolve<IDataService>();
            Project = dataService.GetProjectById(model.ProjectId);
            Task = dataService.GetTaskById(model.Id);
            Date = model.Date;
            Hours = model.Hours;
            Details = model.Details;
            WorkType = model.WorkType;
        }

        public Work ToModel()
        {
            Work.Hours = Hours;
            Work.Date = Date;
            Work.Details = Details;
            Work.WorkType = WorkType;
            Work.ProjectId = Project.Id;
            Work.TaskId = Task.Id;
            return Work;
        }

        public void CycleTaskInstanceType()
        {
            WorkType = WorkType.Cycle();
        }

        public Action<WorkViewModel, DateTime, DateTime> OnDateChanged { get; set; }
        public Action<WorkViewModel> OnHoursChanged { get; set; }
        public Action<WorkViewModel> OnTaskInstanceTypeChanged { get; set; }
        public Action<WorkViewModel> OnDetailsProjectChanged { get; set; }

        public DateTime Date
        {
            get => _date;
            set
            {
                var oldDate = _date;
                var newDate = value;
                _date = value;
                SetProperty(ref _date, value);
                OnDateChanged?.Invoke(this, oldDate, newDate);
            }
        }

        public double Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                SetProperty(ref _hours, value);
                OnHoursChanged?.Invoke(this);
            }
        }

        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                SetProperty(ref _details, value);
                OnDetailsProjectChanged?.Invoke(this);
            }
        }

        public WorkType WorkType
        {
            get => _workType;
            set
            {
                _workType = value;
                SetProperty(ref _workType, value);
                OnTaskInstanceTypeChanged?.Invoke(this);
            }
        }

    }
}
