namespace DailyHours.WebClient.Data
{
    public class TaskElement
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DefaultColor { get; set; }
        public override string ToString() => $"({Code}) {Description}";
    }
}
