using System;

namespace TaskLog.WebClient.Data
{
    public class JobTask
    {
        public int Id { get; set; }
        public ProjectJob ProjectJob { get; set; }
        public TaskType TaskType { get; set; }
        public double Hours { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public override string ToString() => $"{ProjectJob.Code} @ {Date.ToShortDateString()}: {Hours} ({TaskType})";
    }
}
