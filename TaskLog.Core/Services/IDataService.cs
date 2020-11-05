using System;
using System.Collections.Generic;
using TaskLog.Core.Models;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public interface IDataService
    {
        IEnumerable<Project> GetProjects();
        IEnumerable<Task> GetTasks();
        IEnumerable<WorkViewModel> GetWeekWorks(DateTime date);
        IEnumerable<WorkViewModel> GetMonthWorks(DateTime date);
        void SetWorks(IEnumerable<WorkViewModel> workViewModels);

        Project GetProjectById(Guid id);
        Work GetWorkById(Guid id);
        Task GetTaskById(Guid id);
    }
}
