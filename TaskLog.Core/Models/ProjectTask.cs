using System;
using System.Collections.Generic;

namespace TaskLog.Core.Models
{
    public class ProjectTask
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<TaskInstance> TaskInstances { get; set; } = new List<TaskInstance>();
    }
}
