using System;
using System.Collections.Generic;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services.Data
{
    public interface IDataService
    {
        void ReloadData();
        IEnumerable<ProjectViewModel> GetProjects();
        IEnumerable<TaskViewModel> GetTasks();
        IEnumerable<WorkViewModel> GetWeekWorks(DateTime date);
        IEnumerable<WorkViewModel> GetMonthWorks(DateTime date);
        void SetWorks(IEnumerable<WorkViewModel> workViewModels);
        ProjectViewModel GetProjectById(Guid id);
        TaskViewModel GetTaskById(Guid id);
        WorkViewModel GetWorkById(Guid id);
    }
}
