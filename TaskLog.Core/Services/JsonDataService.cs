using System;
using System.Collections.Generic;
using System.Linq;
using TaskLog.Core.Models;
using TaskLog.Core.Utilities;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{

    public class JsonDataService : IDataService
    {
        private Dictionary<Guid, Work> _works;
        private Dictionary<Guid, Project> _projects;
        private Dictionary<Guid, Task> _tasks;
        private readonly IFileConfiguration _fileConfiguration;
        public JsonDataService(IFileConfiguration fileConfiguration)
        {
            _fileConfiguration = fileConfiguration;
            LoadData();
        }

        public void LoadData()
        {
            var projectData = Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Project>>(_fileConfiguration.ProjectsFilename);
            _projects = projectData.ToDictionary(x => x.Id, x => x);

            var taskData = Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Task>>(_fileConfiguration.TasksFilename);
            _tasks = taskData.ToDictionary(x => x.Id, x => x);

            var workData = Wororo.Utilities.JsonSerialization.DeserializeJson<IEnumerable<Work>>(_fileConfiguration.WorksFilename);
            _works = workData.ToDictionary(x => x.Id, x => x);
        }

        public void SaveData()
        {
            var projectData = _projects.Select(x => x.Value);
            Wororo.Utilities.JsonSerialization.SerializeJson(projectData, _fileConfiguration.ProjectsFilename);

            var taskData = _tasks.Select(x => x.Value);
            Wororo.Utilities.JsonSerialization.SerializeJson(taskData, _fileConfiguration.TasksFilename);

            var workData = _works.Select(x => x.Value);
            Wororo.Utilities.JsonSerialization.SerializeJson(workData, _fileConfiguration.WorksFilename);
        }

        public IEnumerable<WorkViewModel> LoadWeekWorks(DateTime date)
        {
            var calendarWeek = date.GetCalendarWeek();
            var taskInstances = _works.Where(x => x.Value.Date.GetCalendarWeek().Equals(calendarWeek));
            return taskInstances.Select(x => new WorkViewModel(x.Value)).ToList();
        }

        public IEnumerable<WorkViewModel> LoadMonthWorks(DateTime date)
        {
            var taskInstances = _works.Select(x => x.Value).Where(x => x.Date.Month.Equals(date.Month));
            return taskInstances.Select(taskInstance => new WorkViewModel(taskInstance)).ToList();
        }

        public void SaveWorks(IEnumerable<WorkViewModel> workViewModels)
        {
            foreach (var taskInstanceViewModel in workViewModels)
                UpdateOrAddTaskInstance(taskInstanceViewModel);
        }

        private void UpdateOrAddTaskInstance(WorkViewModel taskInstanceViewModel)
        {
            var model = taskInstanceViewModel.WriteToModel();

            if (_works.ContainsKey(model.Id))
                _works[model.Id] = model;
            else
                _works.Add(model.Id, model);
        }
    }
}