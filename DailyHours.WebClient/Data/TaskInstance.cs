using System;

namespace DailyHours.WebClient.Data
{
    public class TaskInstance
    {
        public int Id { get; set; }
        public TaskClass TaskClass { get; set; }
        public TaskType TaskType { get; set; }
        public double Hours { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
    }
}
