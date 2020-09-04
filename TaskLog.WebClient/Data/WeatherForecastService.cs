using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace TaskLog.WebClient.Data
{
    public class WeatherForecastService
    {
        private const string JobFile = "jobs.json";
        private const string TaskFile = "tasks.json";
        readonly Calendar _calendar = DateTimeFormatInfo.CurrentInfo.Calendar;
        private int id = 0;
        

        public int GetCurrentCalendarWeek()
        {
            return _calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static void SaveJobs(ProjectJob[] jobs)
        {
            var jobsJson = JsonSerializer.Serialize(jobs);
            System.IO.File.WriteAllText(JobFile, jobsJson);
        }

        public static void LoadJobs()
        {
            var jobsJson = System.IO.File.ReadAllText(JobFile);
            Jobs = JsonSerializer.Deserialize<ProjectJob[]>(jobsJson);
        }

        public static void SaveTasks(JobTask[] tasks)
        {
            var jobs = JsonSerializer.Serialize(tasks);
            System.IO.File.WriteAllText(JobFile, jobs);
        }

        public static void LoadTasks()
        {
            var json = System.IO.File.ReadAllText(JobFile);
            Jobs = JsonSerializer.Deserialize<ProjectJob[]>(json);
        }

        public static IEnumerable<ProjectJob> Jobs { get; set; }
        public JobTask[] GetJobTasks(DateTime taskTime)
        {
            return Enumerable.Range(1, 5).Select(index => GetJobTask(taskTime)).ToArray();
        }

        private readonly Random _rng = new Random();

        public JobTask GetJobTask(DateTime taskTime)
        {
            return new JobTask()
            {
                Id = id++,
                Date = taskTime.Date,
                Hours = (double)(_rng.Next(0, 6)) / 2,
                ProjectJob = Jobs.ElementAt(_rng.Next()%4),
                TaskType = (TaskType)(_rng.Next()%5),
            };
        }
    }
}
