using System;
using System.Collections.Generic;
using TaskLog.Core.Models;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public interface IDataLoaderService
    {
        //void LoadData();
        //void SaveData();
    }
    public interface IDataService
    {
        IEnumerable<WorkViewModel> GetWeekWorks(DateTime date);
        IEnumerable<WorkViewModel> GetMonthWorks(DateTime date);
        void SetWorks(IEnumerable<WorkViewModel> workViewModels);

        Project GetProjectById(Guid id);
        Work GetWorkById(Guid id);
        Task GetTaskById(Guid id);
    }
}
