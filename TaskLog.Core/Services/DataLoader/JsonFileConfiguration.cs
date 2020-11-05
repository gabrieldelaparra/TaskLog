namespace TaskLog.Core.Services.DataLoader {
    public class JsonFileConfiguration : IFileConfiguration
    {
        public string WorksFilename { get; set; } = "Tasks.json";
        public string ProjectsFilename { get; set; } = "Projects.json";
        public string TasksFilename { get; set; } = "ProjectTasks.json";
    }
}