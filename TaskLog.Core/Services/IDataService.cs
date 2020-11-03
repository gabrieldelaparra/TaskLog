using System;
using System.Collections.Generic;
using TaskLog.Core.Models;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public interface IDataService
    {
        void LoadData();
        void SaveData();
        IEnumerable<WorkViewModel> LoadWeekWorks(DateTime date);
        IEnumerable<WorkViewModel> LoadMonthWorks(DateTime date);
        void SaveWorks(IEnumerable<WorkViewModel> workViewModels);

        Project GetProjectById(Guid id);
        Work GetWorkById(Guid id);
        Task GetTaskById(Guid id);
    }
}
