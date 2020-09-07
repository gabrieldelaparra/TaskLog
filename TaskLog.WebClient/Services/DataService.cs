using System;
using System.Collections.Generic;
using System.Drawing;
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
        public DateTime AddBusinessDays(DateTime dateTime, int offset, int extraDays)
        {
            var current = dateTime.AddDays(offset);
            if (current.DayOfWeek.Equals(DayOfWeek.Saturday) || current.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                if (extraDays == 0)
                    extraDays++;
            }


            var sign = Math.Sign(extraDays);
            var unsignedDays = Math.Abs(extraDays);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                }
                while (current.DayOfWeek == DayOfWeek.Saturday ||
                       current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }

        public void SaveJobs(IEnumerable<ProjectJob> jobs)
        {
            var jobsJson = JsonSerializer.Serialize(jobs.ToArray());
            File.WriteAllText(JobFile, jobsJson);
        }

        public void LoadJobs()
        {
            LoadJobs(JobFile);
        }

        private void LoadJobs(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    var jobsJson = File.ReadAllText(filename);
                    Jobs = JsonSerializer.Deserialize<ProjectJob[]>(jobsJson).ToList();
                }
                catch (Exception ex)
                {
                    var now = DateTime.Now;
                    Console.WriteLine(ex);
                    File.Copy(filename, $"{Path.GetFileNameWithoutExtension(filename)}_backup_{now.ToShortDateString()}_{now.ToShortTimeString()}.{Path.GetExtension(filename)}");
                    Jobs = CreateSampleJobs().ToList();
                }
            }
            else
            {
                Jobs = CreateSampleJobs().ToList();
            }
        }

        public void SaveTasks(IEnumerable<JobTask> tasks)
        {
            var tasksJson = JsonSerializer.Serialize(tasks.ToArray());
            File.WriteAllText(TaskFile, tasksJson);
        }

        public void LoadTasks()
        {
            LoadTasks(TaskFile);
        }

        private void LoadTasks(string filename)
        {
            if (File.Exists(TaskFile))
            {
                try
                {
                    var taskJson = File.ReadAllText(filename);
                    Tasks = JsonSerializer.Deserialize<JobTask[]>(taskJson).ToList();
                }
                catch (Exception ex)
                {
                    var now = DateTime.Now;
                    Console.WriteLine(ex);
                    File.Copy(filename, $"{Path.GetFileNameWithoutExtension(filename)}_backup_{now.ToShortDateString()}_{now.ToShortTimeString()}.{Path.GetExtension(filename)}");
                    Tasks = CreateSampleTasks().ToList();
                }
            }
            else
            {
                Tasks = CreateSampleTasks().ToList();
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
                ProjectJob = Jobs.OrderBy(x => x.Id).FirstOrDefault(),
                Hours = 0.5,
                Date = AddBusinessDays(DateTime.Today, offset, 0),
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
        private IEnumerable<JobTask> GetSampleDayTasks(DateTime taskTime)
        {
            return Enumerable.Range(1, 5).Select(index => GetSampleJobTask(taskTime));
        }

        private IEnumerable<JobTask> CreateSampleTasks()
        {
            var tasks = new List<JobTask>();
            tasks.AddRange(GetSampleDayTasks(AddBusinessDays(DateTime.Today, 0, 0)));
            tasks.AddRange(GetSampleDayTasks(AddBusinessDays(DateTime.Today, 0, -1)));
            tasks.AddRange(GetSampleDayTasks(AddBusinessDays(DateTime.Today, 0, +2)));
            return tasks;
        }

        private readonly Random _random = new Random();

        private JobTask GetSampleJobTask(DateTime taskTime)
        {
            return new JobTask()
            {
                Id = Guid.NewGuid(),
                Date = taskTime.Date,
                Hours = (double)(_random.Next(0, 6)) / 2,
                ProjectJob = Jobs.ElementAt(_random.Next() % 4),
                TaskType = (TaskType)(_random.Next() % 5),
            };
        }

        public IEnumerable<ProjectJob> CreateSampleJobs()
        {
            return Enumerable.Range(1, 4).Select(index => CreateSampleJob()).ToArray();
        }

        private ProjectJob CreateSampleJob()
        {
            return new ProjectJob()
            {
                Code = $"Code{_random.Next(0, 127)}",
                Id = Guid.NewGuid(),
                DefaultColor = GetRandomColor(),
                Description = "Short Description",
                ProjectType = (ProjectType)(_random.Next() % 3),
            }
            ;
        }

        //TODO: Create a color palette. Issue #21;
        private string GetRandomColor()
        {
            var names = (ColorPalette[])Enum.GetValues(typeof(ColorPalette));
            var randomColorName = names[_random.Next(names.Length)];
            return randomColorName.ToString();
        }

        public enum ColorPalette
        {
            LIGHTCORAL,
            LIGHTSALMON,
            PINK,
            MOCCASIN,
            PALEGOLDENROD,
            KHAKI,
            LAVENDER,
            PALEGREEN,
            LIGHTCYAN,
            LIGHTSTEELBLUE,
            POWDERBLUE,
            LIGHTSKYBLUE,
            BISQUE,
            LIGHTGRAY,
        }
    }
}
