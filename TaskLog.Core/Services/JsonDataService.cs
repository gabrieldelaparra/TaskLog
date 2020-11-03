using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using AutoMapper;

using TaskLog.Core.Models;
using TaskLog.Core.Utilities;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public class JsonDataService : IDataService
    {
        private readonly IMapper _mapper;
        private Dictionary<Guid, Work> _taskInstances;
        private readonly IFileConfiguration _fileConfiguration;
        public JsonDataService(IFileConfiguration fileConfiguration)
        {
            _fileConfiguration = fileConfiguration;
            _mapper = _config.CreateMapper();
            LoadData();
        }

        readonly MapperConfiguration _config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Work, WorkViewModel>();
            cfg.CreateMap<WorkViewModel, Work>();
        });

        public void LoadData()
        {
            _taskInstances = Wororo.Utilities.JsonSerialization.DeserializeJson<Dictionary<Guid, Work>>(_fileConfiguration.TasksFilename);
        }

        public IEnumerable<WorkViewModel> LoadWeekTaskInstances(DateTime date)
        {
            var calendarWeek = date.GetCalendarWeek();
            var taskInstances = _taskInstances.Where(x => x.Value.Date.GetCalendarWeek().Equals(calendarWeek));
            return taskInstances.Select(x => new WorkViewModel(x.Value)).ToList();
        }

        public IEnumerable<WorkViewModel> LoadMonthTaskInstances(DateTime date)
        {
            var taskInstances = _taskInstances.Select(x => x.Value).Where(x => x.Date.Month.Equals(date.Month));
            return taskInstances.Select(taskInstance => new WorkViewModel(taskInstance)).ToList();
        }

        public void SaveTaskInstances(IEnumerable<WorkViewModel> taskInstanceViewModels)
        {
            foreach (var taskInstanceViewModel in taskInstanceViewModels)
                UpdateOrAddTaskInstance(taskInstanceViewModel);

            Wororo.Utilities.JsonSerialization.SerializeJson(_taskInstances, _fileConfiguration.TasksFilename);
        }

        private void UpdateOrAddTaskInstance(WorkViewModel taskInstanceViewModel)
        {
            var model = taskInstanceViewModel.WriteToModel();

            if (_taskInstances.ContainsKey(model.Id))
                _taskInstances[model.Id] = model;
            else
                _taskInstances.Add(model.Id, model);
        }
    }
}