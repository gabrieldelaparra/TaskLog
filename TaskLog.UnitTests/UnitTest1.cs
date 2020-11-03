using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TaskLog.Core;
using TaskLog.Core.Models;
using TaskLog.Core.Utilities;
using TaskLog.Core.ViewModels;
using Xunit;

namespace TaskLog.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestTaskInstance() {

        }

        [Fact]
        public void TestActionNotifies()
        {
            var vm = new WorkViewModel();
            vm.OnNotifyDateChanged = NotifyDateChanged;
            vm.Date = DateTime.Now.Date;
        }

        private void NotifyDateChanged(WorkViewModel obj, DateTime arg2, DateTime arg3) {
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
