using System;

namespace TaskLog.WebClient.Data
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
        public double Hours { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }
    }
}
