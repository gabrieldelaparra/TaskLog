using System;

namespace TaskLog.Core.Models
{
    public class TaskInstance
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ProjectTaskId { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public string Details { get; set; }
        public TaskInstanceType TaskInstanceType { get; set; } = TaskInstanceType.Normal;
    }
}
