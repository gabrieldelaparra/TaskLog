namespace TaskLog.Core.Services.DataLoader {
    public class JsonFileConfiguration : IFileConfiguration
    {
        public string WorksFilename { get; set; } = "Works.json";
        public string ProjectsFilename { get; set; } = "Projects.json";
        public string TasksFilename { get; set; } = "Tasks.json";
    }
}