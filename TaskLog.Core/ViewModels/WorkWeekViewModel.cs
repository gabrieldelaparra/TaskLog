using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.ViewModels;
using TaskLog.Core.Models;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.ViewModels
{
    public class WorkWeekViewModel: MvxViewModel
    {
        private readonly IList<WorkViewModel> _works = new List<WorkViewModel>();
        public ObservableCollection<WorkDayViewModel> Days { get; private set; } = new ObservableCollection<WorkDayViewModel>();
        private DateTime FirstDay => Days.FirstOrDefault()?.Date ?? DateTime.Today.StartOfWeek();
        private DateTime LastDay => FirstDay.AddDays(4);
        public string Header => $"{FirstDay.ToShortDateString()} - {LastDay.ToShortDateString()}: ({SumHours})";
        public void UpdateWorks(IEnumerable<WorkViewModel> workViewModels) {

            //Clear the collection
            Days.Clear();
            _works.Clear();

            //Update the taskInstance for this week (to calculate SumHours)
            foreach (var workViewModel in workViewModels)
            {
                _works.Add(workViewModel);
                workViewModel.OnHoursChanged = HandleHoursChanged;
                workViewModel.OnDateChanged = HandleDateChanged;
            }

            //Update the days (to display in the view)
            var firstDay = _works.OrderBy(x => x.Date).FirstOrDefault()?.Date ?? DateTime.Today;
            var firstOfWeek = firstDay.StartOfWeek();
            for (var i = 0; i < 5; i++) {
                var worksOfThisDay = _works.Where(x => x.Date.Equals(firstOfWeek.AddDays(i)));
                if (worksOfThisDay.Any()) {
                    var thisDay = worksOfThisDay.FirstOrDefault().Date;
                    var dayViewModel = new WorkDayViewModel(thisDay);
                    dayViewModel.UpdateWorks(worksOfThisDay);
                    Days.Add(dayViewModel);
                }
                else {
                    var dayViewModel = new WorkDayViewModel(firstOfWeek.AddDays(i));
                    Days.Add(dayViewModel);
                }
            }

            //Notify hours changed for the week;
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
            RaisePropertyChanged(() => Header);
        }

        private void HandleDateChanged(WorkViewModel task, DateTime oldDate, DateTime newDate)
        {
            //Filter and update oldDate and newDate Days (if available);
            var affectedDays = Days.Where(x => x.Date.Equals(oldDate) || x.Date.Equals(newDate));
            foreach (var affectedDay in affectedDays) {
                affectedDay.UpdateWorks(_works.Where(x => x.Date.Equals(affectedDay.Date)));
                affectedDay.Date = affectedDay.Date;
            }
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
            RaisePropertyChanged(() => Header);
        }

        private void HandleHoursChanged(WorkViewModel obj)
        {
            RaisePropertyChanged(() => SumHours);
            RaisePropertyChanged(() => IsValidSumHours);
        }

        public double SumHours => _works.Sum(x => x.Hours);
        public bool IsValidSumHours => SumHours == 40.0;
    }
}
