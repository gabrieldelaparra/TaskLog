using System;
using System.Collections.Generic;
using System.IO;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services.DataLoader
{
    public class JsonDataLoaderService : IDataLoaderService
    {
        private readonly IFileConfiguration _fileConfiguration;

        public JsonDataLoaderService(IFileConfiguration fileConfiguration) {
            _fileConfiguration = fileConfiguration;
        }

        public IEnumerable<Project> LoadProjects()
        {
            return File.Exists(_fileConfiguration.ProjectsFilename) 
                ? Array.Empty<Project>() 
                : Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Project>>(_fileConfiguration.ProjectsFilename);
        }

        public IEnumerable<Task> LoadTasks() {
            return File.Exists(_fileConfiguration.TasksFilename) 
                ? Array.Empty<Task>() 
                : Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Task>>(_fileConfiguration.TasksFilename);
        }

        public IEnumerable<Work> LoadWorks() {
            return File.Exists(_fileConfiguration.WorksFilename) 
                ? Array.Empty<Work>() 
                : Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Work>>(_fileConfiguration.WorksFilename);
        }

        public void SaveProjects(IEnumerable<Project> projects) {
            Wororo.Utilities.JsonSerialization.SerializeJson(projects, _fileConfiguration.ProjectsFilename);
        }

        public void SaveTasks(IEnumerable<Task> tasks) {
            Wororo.Utilities.JsonSerialization.SerializeJson(tasks, _fileConfiguration.TasksFilename);
        }

        public void SaveWorks(IEnumerable<Work> works) {
            Wororo.Utilities.JsonSerialization.SerializeJson(works, _fileConfiguration.WorksFilename);
        }
    }
}
