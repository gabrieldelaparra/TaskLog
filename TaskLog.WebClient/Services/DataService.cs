using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using TaskLog.WebClient.Models;

namespace TaskLog.WebClient.Services
{
    public class DataService
    {
        private const string JobFile = "jobs.json";
        private const string TaskFile = "tasks.json";
        readonly Calendar _calendar = DateTimeFormatInfo.CurrentInfo.Calendar;

        public int GetCurrentCalendarWeek()
        {
            return _calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public void SaveJobs(IEnumerable<ProjectJob> jobs)
        {
            var jobsJson = JsonSerializer.Serialize(jobs.ToArray());
            File.WriteAllText(JobFile, jobsJson);
        }

        public void LoadJobs()
        {
            if (File.Exists(JobFile))
            {
                var jobsJson = System.IO.File.ReadAllText(JobFile);
                Jobs = JsonSerializer.Deserialize<ProjectJob[]>(jobsJson).ToList();
            }
        }

        public void SaveTasks(IEnumerable<JobTask> tasks)
        {
            var tasksJson = JsonSerializer.Serialize(tasks.ToArray());
            File.WriteAllText(TaskFile, tasksJson);
        }

        public void LoadTasks()
        {
            if (File.Exists(TaskFile))
            {
                var taskJson = System.IO.File.ReadAllText(TaskFile);
                Tasks = JsonSerializer.Deserialize<JobTask[]>(taskJson).ToList();
            }
            else {
                var tasks = new List<JobTask>();
                tasks.AddRange(GetSampleJobTasks(DateTime.Now));
                tasks.AddRange(GetSampleJobTasks(DateTime.Now.AddDays(-1)));
                tasks.AddRange(GetSampleJobTasks(DateTime.Now.AddDays(+2)));
                Tasks = tasks.ToList();
                OnDataChanged?.Invoke();
                SaveTasks(Tasks);
            }
        }

        public void SaveTasks()
        {
            SaveTasks(Tasks);
        }

        public event Action OnDataChanged;

        public void AddNewTask(int offset)
        {
            Tasks.Add(new JobTask()
            {
                ProjectJob = Jobs.OrderByDescending(x => x.Id).FirstOrDefault(),
                Hours = 0.5,
                Date = DateTime.Today.AddDays(offset),
                TaskType = TaskType.Normal,
                Id = Guid.NewGuid(),
            });
            OnDataChanged?.Invoke();
            SaveTasks(Tasks);
        }

        public void MoveToNewDay(JobTask task, DateTime newTaskDate)
        {
            task.Date = newTaskDate.Date;
            OnDataChanged?.Invoke();
            SaveTasks(Tasks);
        }

        public void RemoveTask(JobTask task)
        {
            Tasks.Remove(task);
            OnDataChanged?.Invoke();
            SaveTasks(Tasks);
        }

        public List<ProjectJob> Jobs { get; set; } = new List<ProjectJob>();
        public List<JobTask> Tasks { get; set; } = new List<JobTask>();
        public JobTask[] GetSampleJobTasks(DateTime taskTime)
        {
            return Enumerable.Range(1, 5).Select(index => GetSampleJobTask(taskTime)).ToArray();
        }

        private readonly Random _rng = new Random();

        public JobTask GetSampleJobTask(DateTime taskTime)
        {
            return new JobTask()
            {
                Id = Guid.NewGuid(),
                Date = taskTime.Date,
                Hours = (double)(_rng.Next(0, 6)) / 2,
                ProjectJob = Jobs.ElementAt(_rng.Next() % 4),
                TaskType = (TaskType)(_rng.Next() % 5),
            };
        }
    }
}
