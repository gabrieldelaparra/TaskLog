using System.Collections.Generic;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services
{
    public class TaskInstanceJsonFileLoaderService : ITaskInstanceLoaderService
    {
        public string Filename { get; set; }
        public TaskInstanceJsonFileLoaderService(string filename) {
            Filename = filename;
        }
        public IEnumerable<TaskInstance> GetTaskInstances() {
            return Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<TaskInstance>>(Filename);
        }
    }
}