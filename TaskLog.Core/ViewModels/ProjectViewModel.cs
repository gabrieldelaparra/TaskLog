using MvvmCross.ViewModels;

using System;

using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class ProjectViewModel : MvxViewModel
    {
        private Project Project { get; }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ProjectType ProjectType { get; set; } = ProjectType.CustomerProject;
        public ProjectViewModel(Project project)
        {
            Project = project;
            FromModel(project);
        }

        public void FromModel(Project model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Code = model.Code;
            ProjectType = model.ProjectType;
        }

        public Project ToModel()
        {
            Project.Name = Name;
            Project.Description = Description;
            Project.Code = Code;
            Project.ProjectType = ProjectType;
            return Project;
        }
        public override string ToString()
        {
            return $"{Code}: {Name}";
        }
    }
}
