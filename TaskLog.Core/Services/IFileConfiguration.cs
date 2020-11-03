namespace TaskLog.Core.Services
{
    public interface IFileConfiguration
    {
        string TasksFilename { get; set; }
        string ProjectsFilename { get; set; }
        string ProjectTasksFilename { get; set; }
    }
}
