using System;
using TaskLog.WebClient.Components;

namespace TaskLog.WebClient.Services
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
