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
            var randomDays = new[] { 1, 2, 3, 4, 5 }.Select(x => _random.Next(2, 5)).Distinct().OrderBy(x=>x).ToArray();
            UpdateWorks(randomDays.Select(x=>new DesignTimeWorkViewModel(firstDayOfThisWeek.AddDays(x))));
        }
    }
}