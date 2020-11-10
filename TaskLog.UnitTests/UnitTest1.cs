using System;
using System.Linq;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Tests;
using TaskLog.Core.Models;
using TaskLog.Core.Services.Data;
using TaskLog.Core.Services.DataLoader;
using TaskLog.Core.Utilities;
using TaskLog.Core.ViewModels;
using Wororo.Utilities;
using Xunit;

namespace TaskLog.UnitTests
{
    public class UnitTest1 : MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            Ioc.ConstructAndRegisterSingleton<IDataAccessService, CodeBasedDataAccessService>();
            Ioc.ConstructAndRegisterSingleton<IDataService, InMemoryDataService>();

            var dataLoaderService = Mvx.IoCProvider.Resolve<IDataAccessService>();
            var project1 = new Project
            {
                ProjectType = ProjectType.CustomerProject,
                Code = "23027",
                Description = "Mekele Dallol project description",
                Name = "Mekele Dallol",
            };
            dataLoaderService.SaveProjects(new []{project1});

            var task1 = new Task
            {
                Description = "HW Engineering",
                Name = "23027-A-21600-210",
                ProjectId = project1.Id,
            };
            dataLoaderService.SaveTasks(new[] { task1 });

            var work1 = new Work()
            {
                Date = DateTime.Today.StartOfWeek(),
                Hours = 3.5,
                Details = "Some description for my work 1",
                ProjectId = project1.Id,
                TaskId = task1.Id,
                WorkType = WorkType.Normal,
            };
            var work2 = new Work()
            {
                Date = DateTime.Today.StartOfWeek(),
                Hours = 4.5,
                Details = "Some description for my work 2",
                ProjectId = project1.Id,
                TaskId = task1.Id,
                WorkType = WorkType.Error,
            };
            var work3 = new Work()
            {
                Date = DateTime.Today.StartOfWeek().AddDays(1),
                Hours = 6,
                Details = "Some description for my work 3",
                ProjectId = project1.Id,
                TaskId = task1.Id,
                WorkType = WorkType.Normal,
            };
            var work4 = new Work()
            {
                Date = DateTime.Today.StartOfWeek().AddDays(3),
                Hours = 4.5,
                Details = "Some description for my work 4",
                ProjectId = project1.Id,
                TaskId = task1.Id,
                WorkType = WorkType.Normal,
            };
            var work5 = new Work()
            {
                Date = DateTime.Today.StartOfWeek().AddDays(3),
                Hours = 3.5,
                Details = "Some description for my work 5",
                ProjectId = project1.Id,
                TaskId = task1.Id,
                WorkType = WorkType.Tools,
            };
            var work6 = new Work()
            {
                Date = DateTime.Today.StartOfWeek().AddDays(4),
                Hours = 8,
                Details = "Some description for my work 6",
                ProjectId = project1.Id,
                TaskId = task1.Id,
                WorkType = WorkType.Rework,
            };
            dataLoaderService.SaveWorks(new[] { work1, work2, work3, work4, work5, work6 });
            var dataService = Mvx.IoCProvider.Resolve<IDataService>();
            dataService.ReloadData();
        }

        [Fact]
        public void TestDependencyInjectionCreateSampleData()
        {
            Setup();
            var dataService = Mvx.IoCProvider.Resolve<IDataService>();

            var projects = dataService.GetProjects();
            Assert.NotNull(projects);
            Assert.Single(projects);
            var tasks = dataService.GetTasks();
            Assert.NotNull(tasks);
            Assert.Single(tasks);
            var works = dataService.GetWeekWorks(DateTime.Today);
            Assert.NotNull(works);
            Assert.Equal(2, works.Count());
        }

        [Fact]
        public void TestGetWorkViewModelFromInMemoryDataService()
        {
            Setup();
            var dataService = Mvx.IoCProvider.Resolve<IDataService>();

            var workViewModel = dataService.GetWeekWorks(DateTime.Today).FirstOrDefault();
            Assert.NotNull(workViewModel);
            Assert.Equal(3.5, workViewModel.Hours);
            Assert.Equal(DateTime.Today, workViewModel.Date);
            Assert.True(workViewModel.Id.ToString().IsNotEmpty());
            Assert.True(workViewModel.Details.IsNotEmpty());
        }

        [Fact]
        public void RunSaveValuesToJsonForWpfTests() {
            Setup();
            var dataService = Mvx.IoCProvider.Resolve<IDataService>();

            var projects = dataService.GetProjects();
            var tasks = dataService.GetTasks();
            var works = dataService.GetWeekWorks(DateTime.Today);

            var jsonDataService = new JsonDataAccessService(new JsonFileConfiguration() {
                ProjectsFilename = @"..\..\..\..\TaskLog.Wpf\bin\Debug\netcoreapp3.1\Projects.json",
                TasksFilename = @"..\..\..\..\TaskLog.Wpf\bin\Debug\netcoreapp3.1\Tasks.json",
                WorksFilename = @"..\..\..\..\TaskLog.Wpf\bin\Debug\netcoreapp3.1\Works.json",
            });
            jsonDataService.SaveTasks(tasks);
            jsonDataService.SaveProjects(projects);
            jsonDataService.SaveWorks(works);
        }

        [Fact]
        public void TestActionNotifies()
        {
            var vm = new WorkViewModel();
            vm.OnDateChanged = HandleDateChanged;
            vm.Date = DateTime.Now.Date;
        }

        private void HandleDateChanged(WorkViewModel obj, DateTime arg2, DateTime arg3) {
            Console.WriteLine($"Date changed: {obj.Date}");
        }

        [Fact]
        public void TestCycleEnumFirstToSecond()
        {
            var length = Enum.GetValues(typeof(WorkType)).Length;
            if (length < 2) 
                Assert.False(true);

            const WorkType typeFirst = (WorkType)0;
            const WorkType typeSecond = (WorkType)1;
            var actual = typeFirst.Cycle();
            Assert.Equal(typeSecond, actual);
        }

        [Fact]
        public void TestCycleEnumLastToFirst()
        {
            var length = Enum.GetValues(typeof(WorkType)).Length;
            if (length < 2)
                Assert.False(true);

            var typeLast = (WorkType)length;
            const WorkType typeFirst = (WorkType)0;
            var actual = typeLast.Cycle();
            Assert.Equal(typeFirst, actual);
        }
    }
}
