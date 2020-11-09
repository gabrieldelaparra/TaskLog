using System;
using System.Collections.Generic;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels.DesignTime
{
    public class DesignTimeWorkDayViewModel : WorkDayViewModel
    {
        private readonly Random _random = new Random();

        public DesignTimeWorkDayViewModel() : this(DateTime.Today) { }

        public DesignTimeWorkDayViewModel(DateTime date)
        {
            Date = date;
            var works = new List<DesignTimeWorkViewModel>();
            for (var i = 0; i < _random.Next(0, 4); i++) {
                works.Add(new DesignTimeWorkViewModel(date));
            }
            UpdateWorks(works);
        }
    }
}