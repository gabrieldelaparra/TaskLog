using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskLog.Core.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid Id { get; set; }
        public ProjectType ProjectType { get; set; } = ProjectType.CustomerProject;
        public IList<Task> Tasks { get; set; } = new List<Task>();
        public IEnumerable<Work> TaskInstances => Tasks.SelectMany(x => x.TaskInstances);
        public override string ToString() => $"({Code}) {Name}: {Description}";
    }
}
