using System;
using System.Collections.Generic;
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
        private readonly Random _random = new Random();
        public List<ProjectJob> Jobs { get; set; } = new List<ProjectJob>();
        public List<JobTask> Tasks { get; set; } = new List<JobTask>();

        public DateTime AddBusinessDays(DateTime dateTime, int offset, int extraDays)
        {
            var current = dateTime.AddDays(offset);
            if (current.DayOfWeek.Equals(DayOfWeek.Saturday) || current.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                if (extraDays == 0)
                {
                    extraDays++;
                }
            }


            var sign = Math.Sign(extraDays);
            var unsignedDays = Math.Abs(extraDays);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                } while (current.DayOfWeek == DayOfWeek.Saturday ||
                         current.DayOfWeek == DayOfWeek.Sunday);
            }

            return current;
        }

        public void AddNewTask(int offset)
        {
            Tasks.Add(new JobTask
            {
                ProjectJob = Jobs.OrderBy(x => x.Id).FirstOrDefault(),
                Hours = 0.5,
                Date = AddBusinessDays(DateTime.Today, offset, 0),
                TaskType = TaskType.Normal,
                Id = Guid.NewGuid()
            });
            OnDataChanged?.Invoke();
            SaveTasks(Tasks);
        }

        public IEnumerable<ProjectJob> CreateSampleJobs()
        {
            return Enumerable.Range(1, 4).Select(index => CreateSampleJob()).ToArray();
        }

        public void LoadJobs()
        {
            LoadJobs(JobFile);
        }

        public void LoadTasks()
        {
            LoadTasks(TaskFile);
        }

        public void MoveToNewDay(JobTask task, DateTime newTaskDate)
        {
            task.Date = newTaskDate.Date;
            OnDataChanged?.Invoke();
            SaveTasks(Tasks);
        }

        public event Action OnDataChanged;

        public void RemoveTask(JobTask task)
        {
            Tasks.Remove(task);
            OnDataChanged?.Invoke();
            SaveTasks(Tasks);
        }

        public void SaveJobs(IEnumerable<ProjectJob> jobs)
        {
            var jobsJson = JsonSerializer.Serialize(jobs.ToArray());
            File.WriteAllText(JobFile, jobsJson);
        }

        public void SaveTasks(IEnumerable<JobTask> tasks)
        {
            var tasksJson = JsonSerializer.Serialize(tasks.ToArray());
            File.WriteAllText(TaskFile, tasksJson);
        }

        public void SaveTasks()
        {
            SaveTasks(Tasks);
        }

        private ProjectJob CreateSampleJob()
        {
            return new ProjectJob
            {
                Code = $"Code_{_random.Next(127)}",
                Id = Guid.NewGuid(),
                DefaultColor = GetRandomColor(),
                Description = "Short Description",
                ProjectType = (ProjectType)_random.Next(Enum.GetValues(typeof(ProjectType)).Length)
            }
                ;
        }

        private IEnumerable<JobTask> CreateSampleTasks()
        {
            var tasks = new List<JobTask>();
            tasks.AddRange(GetSampleDayTasks(AddBusinessDays(DateTime.Today, 0, 0)));
            tasks.AddRange(GetSampleDayTasks(AddBusinessDays(DateTime.Today, 0, -1)));
            tasks.AddRange(GetSampleDayTasks(AddBusinessDays(DateTime.Today, 0, +2)));
            return tasks;
        }

        private string GetRandomColor()
        {
            var names = (ColorPalette[])Enum.GetValues(typeof(ColorPalette));
            var randomColorName = names[_random.Next(names.Length)];
            return randomColorName.ToString();
        }

        private IEnumerable<JobTask> GetSampleDayTasks(DateTime taskTime)
        {
            return Enumerable.Range(1, 5).Select(index => GetSampleJobTask(taskTime));
        }

        private JobTask GetSampleJobTask(DateTime taskTime)
        {
            return new JobTask
            {
                Id = Guid.NewGuid(),
                Date = taskTime.Date,
                Hours = (double)_random.Next(6) / 2,
                ProjectJob = Jobs.ElementAt(_random.Next(4)),
                TaskType = (TaskType)(_random.Next() % Enum.GetValues(typeof(TaskType)).Length)
            };
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
                    File.Copy(filename,
                        $"{Path.GetFileNameWithoutExtension(filename)}_backup_{now.ToShortDateString()}_{now.ToShortTimeString()}.{Path.GetExtension(filename)}");
                    Jobs = CreateSampleJobs().ToList();
                }
            }
            else
            {
                Jobs = CreateSampleJobs().ToList();
            }
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
                    File.Copy(filename,
                        $"{Path.GetFileNameWithoutExtension(filename)}_backup_{now.ToShortDateString()}_{now.ToShortTimeString()}.{Path.GetExtension(filename)}");
                    Tasks = CreateSampleTasks().ToList();
                }
            }
            else
            {
                Tasks = CreateSampleTasks().ToList();
            }
        }

        private enum ColorPalette
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
            LIGHTGRAY
        }
    }
}