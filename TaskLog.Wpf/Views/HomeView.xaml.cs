using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace TaskLog.Wpf.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    //[MvxWindowPresentation(Identifier = nameof(HomeView), Modal = false)]
    public partial class HomeView : MvxWpfView
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}
