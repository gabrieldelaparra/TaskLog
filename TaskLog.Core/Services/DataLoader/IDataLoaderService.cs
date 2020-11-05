using System.Collections.Generic;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services.DataLoader
{
    public interface IDataLoaderService
    {
        IEnumerable<Project> LoadProjects();
        IEnumerable<Task> LoadTasks();
        IEnumerable<Work> LoadWorks();
        void SaveProjects(IEnumerable<Project> projects);
        void SaveTasks(IEnumerable<Task> tasks);
        void SaveWorks(IEnumerable<Work> works);
    }
}
