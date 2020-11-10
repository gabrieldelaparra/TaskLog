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
        private DateTime _date;
        private string _details;
        private double _hours;
        private WorkType _workType = WorkType.Normal;
        public WorkViewModel() { }

        public WorkViewModel(Work work) {
            Work = work;
            FromModel(Work);
        }

        public string Code => $"{Project.Code}-{Task.Code}";

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
                AddOrUpdateData();
            }
        }

        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                SetProperty(ref _details, value);
                OnDetailsChanged?.Invoke(this);
                AddOrUpdateData();
            }
        }

        public string Header => $"({Hours}) [{WorkType}] {Details}";

        public double Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                SetProperty(ref _hours, value);
                OnHoursChanged?.Invoke(this);
                AddOrUpdateData();
            }
        }

        public Guid Id { get; set; }
        public Action<WorkViewModel, DateTime, DateTime> OnDateChanged { get; set; }
        public Action<WorkViewModel> OnDetailsChanged { get; set; }
        public Action<WorkViewModel> OnHoursChanged { get; set; }
        public Action OnProjectChanged { get; set; }
        public Action OnTaskChanged { get; set; }
        public Action<WorkViewModel> OnWorkTypeChanged { get; set; }
        private Project Project { get; set; }
        //public string ProjectCode => Project.Code;
        public string ProjectName => Project.Name;
        private Task Task { get; set; }
        //public string TaskCode => Task.Code;
        public string TaskName => Task.Name;
        private Work Work { get; }

        public WorkType WorkType
        {
            get => _workType;
            set
            {
                _workType = value;
                SetProperty(ref _workType, value);
                OnWorkTypeChanged?.Invoke(this);
                AddOrUpdateData();
            }
        }

        public void CycleTaskInstanceType() {
            WorkType = WorkType.Cycle();
        }

        public Work ToModel() {
            Work.Hours = Hours;
            Work.Date = Date;
            Work.Details = Details;
            Work.WorkType = WorkType;
            Work.ProjectId = Project.Id;
            Work.TaskId = Task.Id;
            return Work;
        }

        private void AddOrUpdateData() {
            var dataService = Mvx.IoCProvider.Resolve<IDataService>();
            dataService.AddOrUpdateWork(ToModel());
        }

        private void FromModel(Work model) {
            var dataService = Mvx.IoCProvider.Resolve<IDataService>();
            Project = dataService.GetProjectById(model.ProjectId);
            Task = dataService.GetTaskById(model.Id);
            Date = model.Date;
            Hours = model.Hours;
            Details = model.Details;
            WorkType = model.WorkType;
        }
    }
}