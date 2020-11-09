using MvvmCross.Platforms.Wpf.Views;
using TaskLog.Core.ViewModels;

namespace TaskLog.Wpf.Views
{
    /// <summary>
    /// Interaction logic for WorkWeekView.xaml
    /// </summary>
    public partial class BoardWeekly_WeekView : MvxWpfView<WorkWeekViewModel>
    {
        public BoardWeekly_WeekView()
        {
            InitializeComponent();
        }
    }
}
