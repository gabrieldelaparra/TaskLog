using MvvmCross.ViewModels;

using System;

using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class TaskViewModel : MvxViewModel
    {
        private Task Task { get; }
        public Guid Id { get; private set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public TaskViewModel(Task task)
        {
            Task = task;
            FromModel(task);
        }

        public void FromModel(Task model)
        {
            Id = model.Id;
            ProjectId = model.ProjectId;
            Name = model.Name;
            Description = model.Description;
            Code = model.Code;
        }

        public Task ToModel()
        {
            Task.Name = Name;
            Task.Description = Description;
            Task.ProjectId = ProjectId;
            Task.Code = Code;
            return Task;
        }
        public override string ToString()
        {
            return $"{Code}: {Name}";
        }
    }
}
