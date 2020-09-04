using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TaskLog.WebClient.Data
{
    public class WeatherForecastService
    {
        Calendar calendar = DateTimeFormatInfo.CurrentInfo.Calendar;
        private int id = 0;
        public List<ProjectTask> GetWeekProjectTasks(IEnumerable<ProjectTask> tasks, int calendarWeek)
        {
            return tasks.Where(x => calendar.GetWeekOfYear(x.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday).Equals(calendarWeek)).ToList();
        }

        public int GetCurrentCalendarWeek()
        {
            return calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        //public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        //{
        //    var rng = new Random();
        //    return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = startDate.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    }).ToArray());
        //}

        public ProjectTask[] GetProjectTasks(DateTime taskTime)
        {
            return Enumerable.Range(1, 5).Select(index => GetProjectTask(taskTime)).ToArray();
        }

        private readonly Random _rng = new Random();

        public ProjectTask GetProjectTask(DateTime taskTime)
        {
            return new ProjectTask()
            {
                Id = id++,
                Name = $"Task {_rng.Next(0, 55)}",
                Hours = (double)(_rng.Next(0, 6)) / 2,
                DateTime = taskTime,
            };
        }
    }
}
