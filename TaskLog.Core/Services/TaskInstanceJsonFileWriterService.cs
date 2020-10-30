using System.Collections.Generic;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services
{
    public class TaskInstanceJsonFileWriterService : ITaskInstanceWriterService
    {
        public string Filename { get; set; }
        public TaskInstanceJsonFileWriterService(string filename)
        {
            Filename = filename;
        }
        public void SaveTaskInstances(IEnumerable<TaskInstance> taskInstances) {
            Wororo.Utilities.JsonSerialization.SerializeJson(taskInstances, Filename);
        }
    }
}