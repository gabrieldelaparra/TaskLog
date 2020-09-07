using System;

namespace TaskLog.WebClient.Models
{
    public class ProjectJob
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DefaultColor { get; set; }
        public ProjectType ProjectType {get;set;}
        public override string ToString() => $"({Code}) {Description}";
    }
    public enum ProjectType {
        ZCAT01 = 0,
        EP0420 = 1,
        EP0430 = 2
    }
}
