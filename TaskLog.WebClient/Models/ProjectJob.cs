using System;

namespace TaskLog.WebClient.Models
{
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