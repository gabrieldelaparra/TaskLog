using System;

namespace TaskLog.Core.Models
{
    public class Work
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TaskId { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public string Details { get; set; }
        public WorkType WorkType { get; set; } = WorkType.Normal;
    }
}
