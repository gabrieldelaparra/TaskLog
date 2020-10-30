using System.Collections.Generic;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services
{
    public interface ITaskInstanceWriterService
    {
        void SaveTaskInstances(IEnumerable<TaskInstance> taskInstances);
    }
}