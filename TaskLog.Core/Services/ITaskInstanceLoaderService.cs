using System;
using System.Collections.Generic;
using System.Text;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services
{
    public interface ITaskInstanceLoaderService
    {
        IEnumerable<TaskInstance> GetTaskInstances();
    }
}
