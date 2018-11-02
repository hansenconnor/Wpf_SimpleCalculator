using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_SimpleCalculator.test;

namespace Wpf_SimpleCalculator
{
    class MainWindowViewModel:BindableBase
    {
        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
        }

        private PortalViewModel portalViewModel = new PortalViewModel();

        private BindableBase _CurrentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public MyICommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {

            switch (destination)
            {
                case "portal":
                    CurrentViewModel = portalViewModel;
                    break;
                case "customers":
                default:
                    CurrentViewModel = portalViewModel;
                    break;
            }
        }
    }
}
