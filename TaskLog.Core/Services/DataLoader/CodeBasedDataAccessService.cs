using System.Collections.Generic;
using TaskLog.Core.Models;

namespace TaskLog.Core.Services.DataLoader
{
    public class CodeBasedDataAccessService : IDataAccessService
    {
        private IEnumerable<Project> _projects = new List<Project>();
        private IEnumerable<Task> _tasks = new List<Task>();
        private IEnumerable<Work> _works = new List<Work>();

        public IEnumerable<Project> LoadProjects() {
            return _projects;
        }

        public IEnumerable<Task> LoadTasks() {
            return _tasks;
        }

        public IEnumerable<Work> LoadWorks() {
            return _works;
        }

        public void SaveProjects(IEnumerable<Project> projects) {
            _projects = projects;
        }

        public void SaveTasks(IEnumerable<Task> tasks) {
            _tasks = tasks;
        }

        public void SaveWorks(IEnumerable<Work> works) {
            _works = works;
        }
    }
}