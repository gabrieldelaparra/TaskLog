using System;
using System.Collections.Generic;
using TaskLog.Core.Models;
//using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services.Data
{
    public interface IDataService
    {
        void ReloadData();
        IEnumerable<Project> GetProjects();
        IEnumerable<Task> GetTasks();
        IEnumerable<Work> GetWeekWorks(DateTime date);
        IEnumerable<Work> GetMonthWorks(DateTime date);
        void SetWorks(IEnumerable<Work> works);
        Project GetProjectById(Guid id);
        Task GetTaskById(Guid id);
        Work GetWorkById(Guid id);
        void AddOrUpdateWork(Work work);
        //IEnumerable<ProjectViewModel> GetProjects();
        //IEnumerable<TaskViewModel> GetTasks();
        //IEnumerable<WorkViewModel> GetWeekWorks(DateTime date);
        //IEnumerable<WorkViewModel> GetMonthWorks(DateTime date);
        //void SetWorks(IEnumerable<WorkViewModel> workViewModels);
        //ProjectViewModel GetProjectById(Guid id);
        //TaskViewModel GetTaskById(Guid id);
        //WorkViewModel GetWorkById(Guid id);
    }
}
