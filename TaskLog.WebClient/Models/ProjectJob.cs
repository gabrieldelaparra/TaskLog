using System;

namespace TaskLog.WebClient.Models
{
    //TODO: Add LastUsedDate and UseCount to add preferences. Issue #16
    public class ProjectJob
    {
        public string Code { get; set; }
        public string DefaultColor { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public ProjectType ProjectType { get; set; }

        public override string ToString()
        {
            return $"({Code}) {Description}";
        }
    }
}