using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using TaskLog.Core.ViewModels;

namespace TaskLog.Wpf.Views
{
    /// <summary>
    /// Interaction logic for DataDisplayView.xaml
    /// </summary>
    //[MvxContentPresentation(WindowIdentifier = nameof(HomeView), StackNavigation = false)]
    //[MvxContentPresentation(WindowIdentifier = nameof(DataDisplayView), StackNavigation = true)]
    //[MvxWindowPresentation(Identifier = nameof(DataDisplayView), Modal = false)]
    public partial class DataDisplayView : IMvxWpfView, IMvxOverridePresentationAttribute
    {
        public DataDisplayView()
        {
            InitializeComponent();
        }

        //public MvxWpfView ActiveView { get; set; }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            var instanceRequest = request as MvxViewModelInstanceRequest;
            var viewModel = instanceRequest?.ViewModelInstance as DataDisplayViewModel;

            return new MvxWindowPresentationAttribute
            {
                Identifier = $"{nameof(DataDisplayView)}"
            };
        }
    }
}
