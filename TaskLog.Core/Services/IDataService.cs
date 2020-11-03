using System;
using System.Collections.Generic;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public interface IDataService
    {
        void LoadData();
        IEnumerable<WorkViewModel> LoadWeekTaskInstances(DateTime date);
        IEnumerable<WorkViewModel> LoadMonthTaskInstances(DateTime date);
        void SaveTaskInstances(IEnumerable<WorkViewModel> taskInstanceViewModels);
        //void UpdateOrAddTaskInstance(TaskInstanceViewModel taskInstanceViewModel);
    }
}
