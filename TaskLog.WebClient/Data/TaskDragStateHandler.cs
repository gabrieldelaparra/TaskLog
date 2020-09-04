using System;
using TaskLog.WebClient.Pages;

namespace TaskLog.WebClient.Data
{
    public class TaskDragStateHandler
    {
        public TaskView TaskView { get; private set; }

        public event Action OnDragSelectionChange;

        public void SetSelectedTask(TaskView taskView)
        {
            TaskView = taskView;
            OnDragSelectionChange?.Invoke();
        }
    }
}
