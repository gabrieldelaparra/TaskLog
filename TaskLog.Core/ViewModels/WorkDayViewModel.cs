using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using MvvmCross.ViewModels;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class WorkDayViewModel : MvxViewModel
    {
        private DateTime _date = DateTime.Today;

        public WorkDayViewModel() {
            
        }

        public WorkDayViewModel(DateTime date) {
            Date = date;
        }
        public ObservableCollection<WorkViewModel> Works { get; private set; } = new ObservableCollection<WorkViewModel>();
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                SetProperty(ref _date, value);
                RaisePropertyChanged(() => Header);
            }
        }

        public string Header => $"{Date.ToShortDateString()}: ({SumHours})";
        public void UpdateWorks(IEnumerable<WorkViewModel> workViewModels)
        {
            Works.Clear();
            foreach (var workViewModel in workViewModels)
            {
                Works.Add(workViewModel);
                workViewModel.OnHoursChanged = HandleHoursChanged;
            }

            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        private void HandleHoursChanged(WorkViewModel task)
        {
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        public double SumHours => Works.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 8.0;

    }
}
