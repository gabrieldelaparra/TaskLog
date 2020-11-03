using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TaskLog.Core.ViewModels;

namespace TaskLog.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<HomeViewModel>();
        }
    }
}
