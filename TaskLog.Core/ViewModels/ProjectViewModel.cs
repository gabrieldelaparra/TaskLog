using MvvmCross.ViewModels;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class ProjectViewModel : MvxViewModel
    {
        private Project Project { get; }
        public ProjectViewModel(Project project)
        {
            Project = project;
        }

        public void ReadFromModel(Project model)
        {
            //Date = Task.Date;
            //Hours = Task.Hours;
            //Details = Task.Details;
            //TaskInstanceType = Task.WorkType;
        }

        public Project WriteToModel()
        {
            //Task.Hours = Hours;
            //Task.Date = Date;
            //Task.Details = Details;
            //Task.WorkType = TaskInstanceType;
            return Project;
        }
    }
}
