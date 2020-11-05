using System.Collections.Generic;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services
{
    public class JsonDataLoaderService : IDataLoaderService
    {
        public string ProjectsFilename { get; set; } = "projects.json";
        public string TasksFilename { get; set; } = "tasks.json";
        public string WorksFilename { get; set; } = "works.json";
        public IEnumerable<Project> LoadProjects() {
            return Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Project>>(ProjectsFilename);
        }

        public IEnumerable<Task> LoadTasks() {
            return Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Task>>(TasksFilename);
        }

        public IEnumerable<Work> LoadWorks() {
            return Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Work>>(WorksFilename);
        }

        public void SaveProjects(IEnumerable<Project> projects) {
            Wororo.Utilities.JsonSerialization.SerializeJson(projects, ProjectsFilename);
        }

        public void SaveTasks(IEnumerable<Task> tasks) {
            Wororo.Utilities.JsonSerialization.SerializeJson(tasks, TasksFilename);
        }

        public void SaveWorks(IEnumerable<Work> works) {
            Wororo.Utilities.JsonSerialization.SerializeJson(works, WorksFilename);
        }
    }
}
