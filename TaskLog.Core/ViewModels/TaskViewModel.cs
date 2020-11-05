using System;
using System.Collections.Generic;
using System.Text;
using TaskLog.Core.Models;

namespace TaskLog.Core.ViewModels
{
    public class TaskViewModel
    {
        public TaskViewModel(Task task)
        {
            Task = task;
        }

        public Task Task { get; }
    }
}
