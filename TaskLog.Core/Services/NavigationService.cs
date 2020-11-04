using System;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public class NavigationService : INavigationService
    {
        private DateTime _navigationDate = DateTime.Today;
        private NavigationType _navigationType = NavigationType.Week;
        public Action OnNavigationDateChange { get; set; }
        public Action OnNavigationTypeChange { get; set; }

        public DateTime NavigationDate
        {
            get => _navigationDate;
            set
            {
                _navigationDate = value;
                OnNavigationDateChange?.Invoke();
            }
        }

        public NavigationType NavigationType
        {
            get => _navigationType;
            set
            {
                _navigationType = value;
                OnNavigationTypeChange?.Invoke();
            }
        }

        //Methods to modify both Type and Date:
        public void ChangeNavigationToMonth() {
            if (NavigationType == NavigationType.Month) return;
            NavigationType = NavigationType.Month;
        }

        public void ChangeNavigationToWeek() {
            if (NavigationType == NavigationType.Week) return;
            NavigationType = NavigationType.Week;
        }

        public void NavigateToNext() {
            NavigationDate = NavigationType switch
            {
                NavigationType.Week => NavigationDate.AddDays(7),
                NavigationType.Month => NavigationDate.AddMonths(1),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public void NavigateToPrevious() {
            NavigationDate = NavigationType switch
            {
                NavigationType.Week => NavigationDate.AddDays(-7),
                NavigationType.Month => NavigationDate.AddMonths(-1),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public void NavigateToToday() {
            NavigationDate = DateTime.Today;
        }
    }
}