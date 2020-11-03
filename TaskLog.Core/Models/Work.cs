using System;

namespace TaskLog.Core.Models
{
    public class Work
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ProjectTaskId { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public string Details { get; set; }
        public WorkType TaskInstanceType { get; set; } = WorkType.Normal;
    }
}
