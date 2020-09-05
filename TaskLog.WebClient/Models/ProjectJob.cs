using System;

namespace TaskLog.WebClient.Models
{
    public class ProjectJob
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DefaultColor { get; set; }
        public override string ToString() => $"({Code}) {Description}";
    }
}
