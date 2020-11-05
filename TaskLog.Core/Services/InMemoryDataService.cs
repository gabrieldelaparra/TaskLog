using System;
using System.Collections.Generic;
using System.Linq;
using TaskLog.Core.Models;
using TaskLog.Core.Utilities;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public class InMemoryDataService : IDataService
    {
        private readonly IDataLoaderService _dataLoaderService;
        private Dictionary<Guid, Project> _projects;
        private Dictionary<Guid, Task> _tasks;
        private Dictionary<Guid, Work> _works;
        public InMemoryDataService(IDataLoaderService dataLoaderService) {
            _dataLoaderService = dataLoaderService;
            ReloadDataFromDisk();
        }

        public void ReloadDataFromDisk() {
            _projects = _dataLoaderService.LoadProjects().ToDictionary(x => x.Id, x => x);
            _tasks = _dataLoaderService.LoadTasks().ToDictionary(x => x.Id, x => x);
            _works = _dataLoaderService.LoadWorks().ToDictionary(x => x.Id, x => x);
        }

        public IEnumerable<Project> GetProjects() {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetTasks() {
            throw new NotImplementedException();
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

        public Project GetProjectById(Guid id)
        {
            if (_projects.ContainsKey(id))
                return _projects[id];
            throw new Exception($"Project not found: {id}");
        }

        public Task GetTaskById(Guid id)
        {
            if (_tasks.ContainsKey(id))
                return _tasks[id];
            throw new Exception($"Task not found: {id}");
        }

        public Work GetWorkById(Guid id)
        {
            if (_works.ContainsKey(id))
                return _works[id];
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
