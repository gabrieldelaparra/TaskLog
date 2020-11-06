using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using TaskLog.Core.Services.Navigation;
using TaskLog.Core.Utilities;

namespace TaskLog.Core.ViewModels
{
    public class NavigationViewModel : MvxViewModel
    {
        private readonly INavigationService _navigationService;
        public IMvxCommand ChangeNavigationToWeekCommand => new MvxCommand(ChangeNavigationToWeek);
        public IMvxCommand ChangeNavigationToMonthCommand => new MvxCommand(ChangeNavigationToMonth);

        public NavigationViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
        }

        private string MonthPeriodLabel => $"Month: {_navigationService.NavigationDate.Month}";

        public string NavigationPeriod
        {
            get
            {
                return _navigationService.NavigationType switch {
                    NavigationType.Week => WeekPeriodLabel,
                    NavigationType.Month => MonthPeriodLabel,
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
        }

        private string WeekPeriodLabel =>
                $"Week: {_navigationService.NavigationDate.StartOfWeek().ToShortDateString()} to {_navigationService.NavigationDate.StartOfWeek().AddDays(5).ToShortDateString()}";

        public void ChangeNavigationToMonth() {
            _navigationService.ChangeNavigationToMonth();
            RaisePropertyChanged(() => NavigationPeriod);
        }

        public void ChangeNavigationToWeek() {
            _navigationService.ChangeNavigationToWeek();
            RaisePropertyChanged(() => NavigationPeriod);
        }

        public void NavigateToNext() {
            _navigationService.NavigateToNext();
            RaisePropertyChanged(() => NavigationPeriod);
        }

        public void NavigateToPrevious() {
            _navigationService.NavigateToPrevious();
            RaisePropertyChanged(() => NavigationPeriod);
        }

        public void NavigateToToday() {
            _navigationService.NavigateToToday();
            RaisePropertyChanged(() => NavigationPeriod);
        }

        //public override void Prepare() {
        //    RaisePropertyChanged(() => NavigationPeriod);
        //}
    }
}