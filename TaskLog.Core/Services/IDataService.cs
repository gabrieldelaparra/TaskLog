using System;
using System.Collections.Generic;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public interface IDataService
    {
        void LoadData();
        IEnumerable<TaskInstanceViewModel> LoadWeekTaskInstances(DateTime date);
        IEnumerable<TaskInstanceViewModel> LoadMonthTaskInstances(DateTime date);
        void SaveTaskInstances(IEnumerable<TaskInstanceViewModel> taskInstanceViewModels);
        void UpdateTaskInstance(TaskInstanceViewModel taskInstanceViewModel);
    }
}
