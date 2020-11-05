using System;
using MvvmCross.Platforms.Wpf.Views;

namespace TaskLog.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MvxWindow
    {
        public MainWindow()
        {
            try {
                InitializeComponent();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
