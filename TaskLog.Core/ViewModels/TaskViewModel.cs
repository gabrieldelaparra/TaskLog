using MvvmCross.ViewModels;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class TaskViewModel : MvxViewModel
    {
        private Task Task { get; }
        public TaskViewModel(Task task)
        {
            Task = task;
        }

        public void ReadFromModel(Task model)
        {
            //Date = Task.Date;
            //Hours = Task.Hours;
            //Details = Task.Details;
            //TaskInstanceType = Task.WorkType;
        }

        public Task WriteToModel()
        {
            //Task.Hours = Hours;
            //Task.Date = Date;
            //Task.Details = Details;
            //Task.WorkType = TaskInstanceType;
            return Task;
        }

    }
}
