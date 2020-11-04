using System.Collections.Generic;

namespace TaskLog.Core.ViewModels
{
    public interface IWorkCollection
    {
        void UpdateTaskInstances(IEnumerable<WorkViewModel> workViewModels);
    }
}