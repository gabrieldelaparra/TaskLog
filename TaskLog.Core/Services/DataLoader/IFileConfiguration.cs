namespace TaskLog.Core.Services.DataLoader
{
    public interface IFileConfiguration
    {
        string WorksFilename { get; set; }
        string ProjectsFilename { get; set; }
        string TasksFilename { get; set; }
    }
}