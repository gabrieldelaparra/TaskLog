using System;
using System.ComponentModel;
using MvvmCross.Platforms.Wpf.Views;
using TaskLog.Core.Models;
using TaskLog.Core.ViewModels;

namespace TaskLog.Wpf.Views
{
    /// <summary>
    /// Interaction logic for WorkDayBoardWeeklyView.xaml
    /// </summary>
    public partial class BoardWeekly_DayView : MvxWpfView<WorkDayViewModel>
    {
        public BoardWeekly_DayView()
        {
            InitializeComponent();
        }

        public WorkDayViewModel DesignTimeViewModel { get; set; } = new WorkDayViewModel()
        {
            Date = DateTime.Today,
            Works = {
                new WorkViewModel {
                    Date = DateTime.Today,
                    WorkType = WorkType.Normal,
                    Hours = 3,
                    Details = "Work 1 Details",
                },
                new WorkViewModel {
                    Date = DateTime.Today,
                    WorkType = WorkType.Error,
                    Hours = 4,
                    Details = "Work 2 Details",
                }
            }
        };
    }
}
