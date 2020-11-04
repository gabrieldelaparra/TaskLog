using System;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core.Services
{
    public interface INavigationService
    {
        Action OnNavigationDateChange { get; set; }
        Action OnNavigationTypeChange { get; set; }
        DateTime NavigationDate { get; set; }
        NavigationType NavigationType { get; set; }
        void ChangeNavigationToMonth();
        void ChangeNavigationToWeek();
        void NavigateToNext();
        void NavigateToPrevious();
        void NavigateToToday();
    }
}
