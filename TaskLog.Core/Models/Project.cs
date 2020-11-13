using System;

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
        //public string Lot { get; set; }
        public Guid Id { get; }
        public ProjectType ProjectType { get; set; } = ProjectType.CustomerProject;
        public override string ToString() => $"({Code}) {Name}: {Description}";
    }
}
