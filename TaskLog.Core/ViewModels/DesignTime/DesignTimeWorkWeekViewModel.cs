using System;
using System.Linq;

namespace TaskLog.Core.ViewModels.DesignTime
{
    public class DesignTimeWorkWeekViewModel : WorkWeekViewModel
    {
        private readonly Random _random = new Random();

        public DesignTimeWorkWeekViewModel() {
            var today = DateTime.Today;
            var firstDayOfThisWeek = Utilities.DateTimeExtensions.StartOfWeek(today);
            var randomDays = new[] { 1, 2, 3, 4, 5 }.Select(x => _random.Next(0, 5)).Distinct().OrderBy(x=>x).ToArray();
            UpdateWorks(randomDays.Select(x=>new DesignTimeWorkViewModel(firstDayOfThisWeek.AddDays(x))));
        }

        public DesignTimeWorkDayViewModel Monday => (DesignTimeWorkDayViewModel)Days[0] ;
        public DesignTimeWorkDayViewModel Tuesday => (DesignTimeWorkDayViewModel)Days[1];
        public DesignTimeWorkDayViewModel Wednesday => (DesignTimeWorkDayViewModel)Days[2];
        public DesignTimeWorkDayViewModel Thursday => (DesignTimeWorkDayViewModel)Days[3];
        public DesignTimeWorkDayViewModel Friday => (DesignTimeWorkDayViewModel)Days[4];
    }
}