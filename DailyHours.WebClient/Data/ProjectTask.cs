using System;

namespace DailyHours.WebClient.Data
{
    public class ProjectTask
    {
        public string Name { get; set; }
        public Project Project { get; set; }
        public double Hours { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
