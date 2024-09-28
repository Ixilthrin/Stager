using System;
using GalaSoft.MvvmLight;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Collections.Generic;
using StagesView;

namespace Stager
{
    class MainWindowVm : ViewModelBase
    {
        public MainWindowVm()
        {
            StagesViewModel = new StagesViewModel();
        }

        public StagesViewModel StagesViewModel { get; set; }

    }
}