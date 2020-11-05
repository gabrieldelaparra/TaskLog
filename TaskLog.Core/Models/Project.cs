using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskLog.Core.Models
{
    public class Project
    {
        public Project() {
            Id = Guid.NewGuid();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid Id { get; }
        public ProjectType ProjectType { get; set; } = ProjectType.CustomerProject;
        //public IList<Guid> WorkIds { get; set; } = new List<Guid>();
        //public IEnumerable<Guid> Works => TaskIds.SelectMany(x => x.WorkIds);
        public override string ToString() => $"({Code}) {Name}: {Description}";
    }
}
