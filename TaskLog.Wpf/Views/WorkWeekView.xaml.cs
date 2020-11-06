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
using TaskLog.Core.ViewModels;

namespace TaskLog.Wpf.Views
{
    /// <summary>
    /// Interaction logic for WorkWeekView.xaml
    /// </summary>
    //[MvxContentPresentation(WindowIdentifier = nameof(DataDisplayView), StackNavigation = false)]
    //[MvxContentPresentation(WindowIdentifier = nameof(DataDisplayView), StackNavigation = true)]
    //[MvxWindowPresentation(Identifier = nameof(WorkWeekView), Modal = false)]
    public partial class WorkWeekView : MvxWpfView
    {
        public WorkWeekView()
        {
            InitializeComponent();
        }
    }
}
