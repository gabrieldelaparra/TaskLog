using System;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels.DesignTime
{
    public class DesignTimeWorkViewModel : WorkViewModel
    {
        private static readonly Random Random = new Random();

        public DesignTimeWorkViewModel() : this(DateTime.Today) { }

        public DesignTimeWorkViewModel(DateTime date) : base(new Work()
        {
            Details = $"Random Task Details {Random.Next(1, 100)}",
            Date = date,
            Hours = (Random.Next(0, 16) * .5) / 2,
            WorkType = (WorkType)Random.Next(0, 5),
        })
        { }
    }
}
