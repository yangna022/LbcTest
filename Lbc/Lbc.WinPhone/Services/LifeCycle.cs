using Lbc.Services;
using Lbc.WinPhone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LifeCycle))]
namespace Lbc.WinPhone.Services {
    public class LifeCycle : ILifeCycle {
        public void Exit() {
            App.Current.Terminate();
        }
    }
}
