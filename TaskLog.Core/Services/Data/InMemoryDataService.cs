using System;
using System.Collections.Generic;
using System.Linq;
using TaskLog.Core.Models;
using TaskLog.Core.Services.DataLoader;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.Services.Data
{
    public class InMemoryDataService : IDataService
    {
        private readonly IDataAccessService _dataAccessService;
        private Dictionary<Guid, Project> _projects = new Dictionary<Guid, Project>();
        private Dictionary<Guid, Task> _tasks = new Dictionary<Guid, Task>();
        private Dictionary<Guid, Work> _works = new Dictionary<Guid, Work>();
        public InMemoryDataService(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
            ReloadData();
        }

        public void ReloadData()
        {
            var loadProjects = _dataAccessService.LoadProjects();
            var loadTasks = _dataAccessService.LoadTasks();
            var loadWorks = _dataAccessService.LoadWorks();

            _projects = loadProjects.ToDictionary(x => x.Id, x => x);
            _tasks = loadTasks.ToDictionary(x => x.Id, x => x);
            _works = loadWorks.ToDictionary(x => x.Id, x => x);
        }

        public IEnumerable<Project> GetProjects()
        {
            return _projects.Select(x => x.Value);
        }

        public IEnumerable<Task> GetTasks()
        {
            return _tasks.Select(x => x.Value);
        }

        public IEnumerable<Work> GetWeekWorks(DateTime date)
        {
            var calendarWeek = date.GetCalendarWeek();
            var works = _works.Where(x => x.Value.Date.GetCalendarWeek().Equals(calendarWeek));
            return works.Select(x => x.Value);
        }

        public IEnumerable<Work> GetMonthWorks(DateTime date)
        {
            var works = _works.Select(x => x.Value).Where(x => x.Date.Month.Equals(date.Month));
            return works;
        }

        public void SetWorks(IEnumerable<Work> works)
        {
            foreach (var taskInstanceViewModel in works)
                AddOrUpdateWork(taskInstanceViewModel);
        }

        //TODO: SetTasks
        //TODO: SetProjects
        //TODO: Save using IDataLoaderService

        public Project GetProjectById(Guid id) {
            if (_projects.ContainsKey(id))
                return _projects[id];
            throw new Exception($"Project not found: {id}");
        }

        public Task GetTaskById(Guid id) {
            if (_tasks.ContainsKey(id))
                return _tasks[id];
            throw new Exception($"Task not found: {id}");
        }

        public Work GetWorkById(Guid id) {
            if (_works.ContainsKey(id))
                return _works[id];
            throw new Exception($"Work not found: {id}");
        }

        public void AddOrUpdateWork(Work work)
        {
            if (_works.ContainsKey(work.Id))
                _works[work.Id] = work;
            else
                _works.Add(work.Id, work);
        }
    }
}
