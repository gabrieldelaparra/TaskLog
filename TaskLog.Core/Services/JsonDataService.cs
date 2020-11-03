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
        private List<TaskInstance> _taskInstances;
        private readonly IFileConfiguration _fileConfiguration;
        public JsonDataService(IFileConfiguration fileConfiguration)
        {
            _fileConfiguration = fileConfiguration;
            _mapper = _config.CreateMapper();
            LoadData();
        }

        readonly MapperConfiguration _config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TaskInstance, TaskInstanceViewModel>();
            cfg.CreateMap<TaskInstanceViewModel, TaskInstance>();
        });

        public void LoadData()
        {
            _taskInstances = Wororo.Utilities.JsonSerialization.DeserializeJson<List<TaskInstance>>(_fileConfiguration.Filename);
        }

        public IEnumerable<TaskInstanceViewModel> LoadWeekTaskInstances(DateTime date)
        {
            var calendarWeek = date.GetCalendarWeek();
            var taskInstances = _taskInstances.Where(x => x.Date.GetCalendarWeek().Equals(calendarWeek));

            var viewModels = new List<TaskInstanceViewModel>();
            foreach (var taskInstance in taskInstances)
            {
                viewModels.Add(_mapper.Map<TaskInstance, TaskInstanceViewModel>(taskInstance));
            }

            return viewModels;
        }

        public IEnumerable<TaskInstanceViewModel> LoadMonthTaskInstances(DateTime date)
        {
            var taskInstances = _taskInstances.Where(x => x.Date.Month.Equals(date.Month));

            var viewModels = new List<TaskInstanceViewModel>();
            foreach (var taskInstance in taskInstances)
            {
                viewModels.Add(_mapper.Map<TaskInstance, TaskInstanceViewModel>(taskInstance));
            }

            return viewModels;
        }

        public void SaveTaskInstances(IEnumerable<TaskInstanceViewModel> taskInstanceViewModels)
        {
            foreach (var taskInstanceViewModel in taskInstanceViewModels) {
                if (_taskInstances.Any(x => x.Id.Equals(taskInstanceViewModel.Id))) {
                    var taskInstance = _taskInstances.First(x => x.Id.Equals(taskInstanceViewModel.Id));
                    taskInstance =_mapper.Map<TaskInstanceViewModel, TaskInstance>(taskInstanceViewModel);
                }
                else {
                    var taskInstance =_mapper.Map<TaskInstanceViewModel, TaskInstance>(taskInstanceViewModel);
                    _taskInstances.Add(taskInstance);
                }
            }

            Wororo.Utilities.JsonSerialization.SerializeJson(_taskInstances, _fileConfiguration.Filename);
        }

        public void UpdateTaskInstance(TaskInstanceViewModel taskInstanceViewModel)
        {
            throw new NotImplementedException();
        }
    }
}