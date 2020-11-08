using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using TaskLog.Core.ViewModels;

namespace TaskLog.Wpf.Views
{
    /// <summary>
    /// Interaction logic for WorkView.xaml
    /// </summary>
    //[MvxContentPresentation(WindowIdentifier = nameof(DataDisplayView), StackNavigation = false)]
    public partial class WorkView : MvxWpfView, IMvxOverridePresentationAttribute
    {
        public WorkView()
        {
            InitializeComponent();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            var instanceRequest = request as MvxViewModelInstanceRequest;
            var viewModel = instanceRequest?.ViewModelInstance as WorkViewModel;

            return new MvxContentPresentationAttribute
            {
                WindowIdentifier = $"{nameof(DataDisplayView)}",
                StackNavigation = false
            };
            //throw new NotImplementedException();
        }
    }
}
