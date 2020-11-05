using System;
using System.Collections.Generic;
using System.Text;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services
{
    public interface IDataLoaderService
    {
        string WorksFilename { get; set; }
        string ProjectsFilename { get; set; }
        string TasksFilename { get; set; }

        IEnumerable<Project> LoadProjects();
        IEnumerable<Task> LoadTasks();
        IEnumerable<Work> LoadWorks();

         void SaveProjects(IEnumerable<Project> projects);
         void SaveTasks(IEnumerable<Task> projects);
         void SaveWorks(IEnumerable<Work> projects);
    }
}
