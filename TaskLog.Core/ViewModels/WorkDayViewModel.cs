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

        public ObservableCollection<WorkViewModel> Works { get; private set; } = new ObservableCollection<WorkViewModel>();
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                SetProperty(ref _date, value);
            }
        }
        public string TestString { get; set; } = "Test: WorkDayViewModel";
        public string Header { get; set; }
        public void UpdateWorks(IEnumerable<WorkViewModel> workViewModels)
        {
            if (!workViewModels.Any())
                return;

            Works.Clear();
            foreach (var work in workViewModels)
            {
                Works.Add(work);
                work.OnNotifyHoursChanged = HandleNotifyHoursChanged;
            }
            Header = Date.ToShortDateString();

            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
            RaisePropertyChanged(() => Header);
        }

        private void HandleNotifyHoursChanged(WorkViewModel task)
        {
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        public double SumHours => Works.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 8.0;

    }
}
