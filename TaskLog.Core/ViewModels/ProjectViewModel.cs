using System;
using System.Collections.Generic;
using System.Text;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(Project project)
        {
            Project = project;
        }

        public Project Project { get; }
    }
}
