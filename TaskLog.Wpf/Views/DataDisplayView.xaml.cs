using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace TaskLog.Wpf.Views
{
    /// <summary>
    /// Interaction logic for DataDisplayView.xaml
    /// </summary>
    //[MvxContentPresentation(WindowIdentifier = nameof(HomeView), StackNavigation = false)]
    //[MvxContentPresentation(WindowIdentifier = nameof(DataDisplayView), StackNavigation = false)]
    //[MvxWindowPresentation(Identifier = nameof(DataDisplayView), Modal = false)]
    public partial class DataDisplayView : MvxWpfView
    {
        public DataDisplayView()
        {
            InitializeComponent();
        }
    }
}
