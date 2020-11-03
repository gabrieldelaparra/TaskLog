namespace TaskLog.Core.Services {
    public class JsonFileConfiguration : IFileConfiguration
    {
        public string TasksFilename { get; set; } = "Tasks.json";
        public string ProjectsFilename { get; set; } = "Projects.json";
        public string ProjectTasksFilename { get; set; } = "ProjectTasks.json";
    }
}