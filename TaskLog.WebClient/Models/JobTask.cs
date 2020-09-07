using System;

namespace TaskLog.WebClient.Models
{
    public class JobTask
    {
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public string Description { get; set; }
        public double Hours { get; set; }
        public Guid Id { get; set; }
        public ProjectJob ProjectJob { get; set; }
        public TaskType TaskType { get; set; }
        public override string ToString()
        {
            return $"{ProjectJob.Code} @ {Date.ToShortDateString()}: {Hours} ({TaskType})";
        }
    }
}