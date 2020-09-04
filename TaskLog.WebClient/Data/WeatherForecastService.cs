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
        //public List<ProjectTask> GetWeekProjectTasks(IEnumerable<ProjectTask> tasks, int calendarWeek)
        //{
        //    return tasks.Where(x => calendar.GetWeekOfYear(x.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday).Equals(calendarWeek)).ToList();
        //}

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

        public static IEnumerable<TaskClass> TaskClasses =>
            new List<TaskClass>() {
                new TaskClass(){Id = 1, Description = "Project 1 SW", Code = "32547-A-210-400", DefaultColor = "LIGHTCORAL"},
                new TaskClass(){Id = 2, Description = "Project 300 HW", Code = "78548-A-210-400", DefaultColor = "MOCCASIN"},
                new TaskClass(){Id = 3, Description = "Project 16513 SW", Code = "65894-A-210-400", DefaultColor = "LIGHTCYAN"},
                new TaskClass(){Id = 4, Description = "Project 54217 SW", Code = "54127-A-210-400", DefaultColor = "GAINSBORO"},

            };

        //public ProjectTask[] GetProjectTasks(DateTime taskTime)
        //{
        //    return Enumerable.Range(1, 5).Select(index => GetProjectTask(taskTime)).ToArray();
        //}

        public TaskInstance[] GetTaskInstances(DateTime taskTime)
        {
            return Enumerable.Range(1, 5).Select(index => GetTaskInstance(taskTime)).ToArray();
        }

        private readonly Random _rng = new Random();

        public TaskInstance GetTaskInstance(DateTime taskTime)
        {
            return new TaskInstance()
            {
                Id = id++,
                Date = taskTime.Date,
                Hours = (double)(_rng.Next(0, 6)) / 2,
                TaskClass = TaskClasses.ElementAt(_rng.Next()%4),
                TaskType = (TaskType)(_rng.Next()%5),
            };
        }

        //public ProjectTask GetProjectTask(DateTime taskTime)
        //{
        //    return new ProjectTask()
        //    {
        //        Id = id++,
        //        Name = $"Task {_rng.Next(0, 55)}",
        //        Hours = (double)(_rng.Next(0, 6)) / 2,
        //        DateTime = taskTime,
        //    };
        //}
    }
}
