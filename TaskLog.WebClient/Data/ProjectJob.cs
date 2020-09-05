using System;

namespace TaskLog.WebClient.Data
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
