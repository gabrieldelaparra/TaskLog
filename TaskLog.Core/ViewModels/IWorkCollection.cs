using System.Collections.Generic;

namespace TaskLog.Core.ViewModels
{
    public interface IWorkCollection
    {
        void UpdateWorks(IEnumerable<WorkViewModel> workViewModels);
    }
}