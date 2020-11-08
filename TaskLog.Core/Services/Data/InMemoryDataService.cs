using System;
using System.Collections.Generic;
using System.Linq;

using TaskLog.Core.Models;
using TaskLog.Core.Services.DataLoader;
using TaskLog.Core.Utilities;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services.Data
{
    public class InMemoryDataService : IDataService
    {
        private readonly IDataLoaderService _dataLoaderService;
        private Dictionary<Guid, Project> _projects = new Dictionary<Guid, Project>();
        private Dictionary<Guid, Task> _tasks = new Dictionary<Guid, Task>();
        private Dictionary<Guid, Work> _works = new Dictionary<Guid, Work>();
        public InMemoryDataService(IDataLoaderService dataLoaderService)
        {
            _dataLoaderService = dataLoaderService;
            ReloadData();
        }

        public void ReloadData()
        {
            var loadProjects = _dataLoaderService.LoadProjects();
            var loadTasks = _dataLoaderService.LoadTasks();
            var loadWorks = _dataLoaderService.LoadWorks();
            if (loadProjects != null && loadProjects.Any())
                _projects = loadProjects.ToDictionary(x => x.Id, x => x);
            if (loadTasks != null && loadTasks.Any())
                _tasks = loadTasks.ToDictionary(x => x.Id, x => x);
            if (loadWorks != null && loadWorks.Any())
                _works = loadWorks.ToDictionary(x => x.Id, x => x);
        }

        public IEnumerable<ProjectViewModel> GetProjects()
        {
            return _projects.Select(x => new ProjectViewModel(x.Value)).ToList();
        }

        public IEnumerable<TaskViewModel> GetTasks()
        {
            return _tasks.Select(x => new TaskViewModel(x.Value)).ToList();
        }

        public IEnumerable<WorkViewModel> GetWeekWorks(DateTime date)
        {
            var calendarWeek = date.GetCalendarWeek();
            var taskInstances = _works.Where(x => x.Value.Date.GetCalendarWeek().Equals(calendarWeek));
            return taskInstances.Select(x => new WorkViewModel(x.Value)).ToList();
        }

        public IEnumerable<WorkViewModel> GetMonthWorks(DateTime date)
        {
            var taskInstances = _works.Select(x => x.Value).Where(x => x.Date.Month.Equals(date.Month));
            return taskInstances.Select(taskInstance => new WorkViewModel(taskInstance)).ToList();
        }

        public void SetWorks(IEnumerable<WorkViewModel> workViewModels)
        {
            foreach (var taskInstanceViewModel in workViewModels)
                UpdateOrAddWork(taskInstanceViewModel);
        }

        public ProjectViewModel GetProjectById(Guid id)
        {
            if (_projects.ContainsKey(id))
                return new ProjectViewModel(_projects[id]);
            throw new Exception($"Project not found: {id}");
        }

        public TaskViewModel GetTaskById(Guid id)
        {
            if (_tasks.ContainsKey(id))
                return new TaskViewModel(_tasks[id]);
            throw new Exception($"Task not found: {id}");
        }

        public WorkViewModel GetWorkById(Guid id)
        {
            if (_works.ContainsKey(id))
                return new WorkViewModel(_works[id]);
            throw new Exception($"Work not found: {id}");
        }

        private void UpdateOrAddWork(WorkViewModel taskInstanceViewModel)
        {
            var model = taskInstanceViewModel.WriteToModel();

            if (_works.ContainsKey(model.Id))
                _works[model.Id] = model;
            else
                _works.Add(model.Id, model);
        }
    }
}
